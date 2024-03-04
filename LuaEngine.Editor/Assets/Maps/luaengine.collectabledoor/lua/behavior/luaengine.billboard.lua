-- Any code outside of functions is executed immediately.
local updateCallback

-- Called every frame the game is unpaused.
local function Update()
	local camera = WorldHandler.CurrentCamera
	if not camera then return end
	local cameraRotation = camera.GetEulerAngles()
	script.GameObject.SetEulerAngles(cameraRotation)
end

-- Called when the script or object holding the script is destroyed.
local function OnDestroy()
    Core.OnUpdate.Remove(updateCallback)
end

updateCallback = Core.OnUpdate.Add(Update)
script.OnDestroy.Add(OnDestroy)