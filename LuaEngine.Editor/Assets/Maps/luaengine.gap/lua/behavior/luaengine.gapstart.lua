-- Gap Script.

-- Get the script for the end gap trigger.
local gapEndScript = script.GameObject.GetGameObjectValue("luaengine.gap.endtrigger").Value.GetScriptBehavior("luaengine.gapend.lua")
local gapName = script.GameObject.GetStringValue("luaengine.gap.name").Value
local gapScore = script.GameObject.GetNumberValue("luaengine.gap.score").Value

-- Don't let us do the gap multiple times in the same combo.
local gapStarted = false
local gapUsed = false

local updateCallback
local initializationCallback

-- When the player touches the gap end trigger.
local function OnPlayerTriggerEnd(player)
	-- Must be in the air
	if player.Grounded then return end
	if player.IsAI then return end
	if not gapStarted then return end
	if gapUsed then return end
	gapUsed = true
	player.AddScoreMultiplier()
	player.DoTrick(gapName, gapScore)
end

-- When the player touches the gap start trigger.
local function OnPlayerTriggerStart(player)
	-- Must be in the air
	if player.Grounded then return end
	if player.IsAI then return end
	if gapUsed then return end
	gapStarted = true
end

-- Allow player to do the gap again when they end their combo.
local function OnPlayerComboEnded()
	gapUsed = false
	gapStarted = false
end

-- Cancel the gap if the player touches the floor.
local function Update()
	local player = WorldHandler.CurrentPlayer
	if not player then return end
	if player.Grounded then
		gapStarted = false
	end
end

-- Delete callbacks when we're destroyed.
local function OnDestroy()
    Core.OnUpdate.Remove(updateCallback)
	StageManager.OnStagePostInitialization.Remove(initializationCallback)
end

-- Initialize all callbacks.
local function OnStagePostInitialization()
	updateCallback = Core.OnUpdate.Add(Update)
	script.OnDestroy.Add(OnDestroy)

	script.OnPlayerTriggerEnter.Add(OnPlayerTriggerStart)
	gapEndScript.OnPlayerTriggerEnter.Add(OnPlayerTriggerEnd)
	
	WorldHandler.CurrentPlayer.OnLandCombo.Add(OnPlayerComboEnded)
	WorldHandler.CurrentPlayer.OnDropCombo.Add(OnPlayerComboEnded)
end

-- We initialize here rather than OnStart to make sure the player is already initialized.
initializationCallback = StageManager.OnStagePostInitialization.Add(OnStagePostInitialization)