-- This controls the whole collectable loop - what happens when a collectable is collected and when all of them are collected.

-- Get the PlayableDirector for the collected all collectables cutscene.
local CollectableDirector = script.GameObject.GetComponentValue("luaengine.collectabledirector").Value

-- Set up global variables.
CollectableExample = {}

CollectableExample.TotalCollectables = 0
CollectableExample.Collected = 0

-- Called when a collectable is collected. Starts the cutscene if all collectables have been collected.
function CollectableExample.Collect()
	CollectableExample.Collected = CollectableExample.Collected + 1
	if CollectableExample.Collected == CollectableExample.TotalCollectables then
		SequenceHandler.StartSequence(CollectableDirector)
	end
end

-- Resets all collectables and the cutscene back to the start.
function CollectableExample.Reset()
	CollectableExample.Collected = 0
	-- Find all objects with the "luaengine.collectable.lua" script.
	local collectables = Engine.FindScriptBehaviors("luaengine.collectable.lua", true)
	for k, v in pairs(collectables) do
		local visuals = v.GameObject.GetGameObjectValue("luaengine.collectable.visuals").Value
		visuals.Active = true
	end
	CollectableDirector.SkipTo(0)
end