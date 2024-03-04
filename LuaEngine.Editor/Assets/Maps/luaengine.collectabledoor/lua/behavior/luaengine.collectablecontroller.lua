local CollectableDirector = script.GameObject.GetComponentValue("luaengine.collectabledirector").Value

CollectableExample = {}

CollectableExample.TotalCollectables = 0
CollectableExample.Collected = 0

function CollectableExample.Collect()
	CollectableExample.Collected = CollectableExample.Collected + 1
	if CollectableExample.Collected == CollectableExample.TotalCollectables then
		SequenceHandler.StartSequence(CollectableDirector)
	end
end

function CollectableExample.Reset()
	CollectableExample.Collected = 0
	local collectables = Engine.FindScriptBehaviors("luaengine.collectable.lua", true)
	for k, v in pairs(collectables) do
		v.GameObject.Active = true
	end
	CollectableDirector.SkipTo(0)
end