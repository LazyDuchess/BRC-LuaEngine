-- Resets collectables when the player touches the trigger.

local function OnPlayerTriggerEnter(player)
	if player.IsAI then return end
	CollectableExample.Reset()
end

script.OnPlayerTriggerEnter.Add(OnPlayerTriggerEnter)