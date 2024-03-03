--PRIORITY: -100

Vector3 = {}

function Vector3.New(x, y, z)
	local vec3 = {
		x = x,
		y = y,
		z = z
	}
	return vec3
end

function Vector3.Multiply(vec, multiplier)
	vec.x = vec.x * multiplier
	vec.y = vec.y * multiplier
	vec.z = vec.z * multiplier
	return vec;
end

function Vector3.Divide(vec, divisor)
	vec.x = vec.x / divisor
	vec.y = vec.y / divisor
	vec.z = vec.z / divisor
end

function Vector3.Add(v1, v2)
	v1.x = v1.x + v2.x
	v1.y = v1.y + v2.y
	v1.z = v1.z + v2.z
	return v1;
end

function Vector3.Subtract(v1, v2)
	v1.x = v1.x - v2.x
	v1.y = v1.y - v2.y
	v1.z = v1.z - v2.z
	return v1;
end

function Vector3.Length(vec)
	return math.sqrt(vec.x*vec.x+vec.y*vec.y+vec.z*vec.z)
end

function Vector3.Normalize(vec)
	local length = Vector3.Length(vec)
	if length ~= 0 then
		return Vector3.Divide(vec, length)
	end
	return vec
end

function Vector3.Distance(v1, v2)
	return Vector3.Length(Vector3.Subtract(v1, v2))
end

function Vector3.Dot(v1, v2)
	return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z
end

Vector3.Up = Vector3.New(0, 1, 0)
Vector3.Down = Vector3.New(0, -1, 0)

Vector3.Right = Vector3.New(1, 0, 0)
Vector3.Left = Vector3.New(-1, 0, 0)

Vector3.Forward = Vector3.New(0, 0, 1)
Vector3.Backward = Vector3.New(0, 0, -1)