local gapEndScript = script.GameObject.GetGameObjectValue("luaengine.gap.endtrigger").Value.GetScriptBehavior("luaengine.gapend.lua")
local gapName = script.GameObject.GetStringValue("luaengine.gap.name").Value
local gapScore = script.GameObject.GetNumberValue("luaengine.gap.score").Value

local gapStarted = false
local gapUsed = false

local updateCallback
local initializationCallback

local function OnPlayerTriggerEnd(player)
	if player.Grounded then return end
	if player.IsAI then return end
	if not gapStarted then return end
	if gapUsed then return end
	gapUsed = true
	player.AddScoreMultiplier()
	player.DoTrick(gapName, gapScore)
end

local function OnPlayerTriggerStart(player)
	if player.Grounded then return end
	if player.IsAI then return end
	if gapUsed then return end
	gapStarted = true
end

local function OnPlayerComboEnded()
	gapUsed = false
	gapStarted = false
end

local function Update()
	local player = WorldHandler.CurrentPlayer
	if not player then return end
	if player.Grounded then
		gapStarted = false
	end
end

local function OnDestroy()
    Core.OnUpdate.Remove(updateCallback)
	StageManager.OnStagePostInitialization.Remove(initializationCallback)
end

local function OnStagePostInitialization()
	updateCallback = Core.OnUpdate.Add(Update)
	script.OnDestroy.Add(OnDestroy)

	script.OnPlayerTriggerEnter.Add(OnPlayerTriggerStart)
	gapEndScript.OnPlayerTriggerEnter.Add(OnPlayerTriggerEnd)
	
	WorldHandler.CurrentPlayer.OnLandCombo.Add(OnPlayerComboEnded)
	WorldHandler.CurrentPlayer.OnDropCombo.Add(OnPlayerComboEnded)
end

initializationCallback = StageManager.OnStagePostInitialization.Add(OnStagePostInitialization)