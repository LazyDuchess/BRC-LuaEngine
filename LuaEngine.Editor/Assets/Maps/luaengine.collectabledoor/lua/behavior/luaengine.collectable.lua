local audioSource = script.GameObject.GetComponentValue("luaengine.collectable.audiosource").Value
local visuals = script.GameObject.GetGameObjectValue("luaengine.collectable.visuals").Value

-- Called before the first frame update.
local function Start()
	-- Increase the total collectables global to account for this one.
	CollectableExample.TotalCollectables = CollectableExample.TotalCollectables + 1
end

local function OnPlayerTriggerEnter(player)
	if player.IsAI then return end
	if not visuals.Active then return end
	audioSource.Play()
	CollectableExample.Collect()
	visuals.Active = false
end

script.OnStart.Add(Start)
script.OnPlayerTriggerEnter.Add(OnPlayerTriggerEnter)