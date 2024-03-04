-- Called before the first frame update.
local function Start()
	CollectableExample.TotalCollectables = CollectableExample.TotalCollectables + 1
end

local function OnPlayerTriggerEnter(player)
	if player.IsAI then return end
	CollectableExample.Collect()
	script.GameObject.Active = false
end

script.OnStart.Add(Start)
script.OnPlayerTriggerEnter.Add(OnPlayerTriggerEnter)