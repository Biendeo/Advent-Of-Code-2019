$Input = Get-Content (Join-Path -Path input -ChildPath 01.txt)

function Get-FuelCount([Int64]$Mass) {
	return [math]::floor($Mass / 3) - 2
}

$TotalMass = 0

foreach ($Mass in $Input) {
	$Mass = [int]::Parse($Mass)
	while ($Mass -gt 6) {
		$Mass = Get-FuelCount($Mass)
		$TotalMass += $Mass
	}
}

$TotalMass