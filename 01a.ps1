$Input = Get-Content (Join-Path -Path input -ChildPath 01.txt)

function Get-FuelCount([int]$Mass) {
	return [math]::floor($Mass / 3) - 2
}

$TotalMass = 0

foreach ($Mass in $Input) {
	$TotalMass += Get-FuelCount($Mass)
}

$TotalMass