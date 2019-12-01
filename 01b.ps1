param (
	[string]$InputFile,
	[int]$MassValue
)

function Get-FuelCount([Int64]$Mass) {
	return [math]::floor($Mass / 3) - 2
}

if (!$InputFile -and !$MassValue) {
	Write-Output "Please specify either a mass value or a file with a bunch of masses"
	exit
}

if ($InputFile) {
	$Input = [int[]](Get-Content $InputFile | foreach { [int]$_ })
} else {
	$Input = [int[]]@($MassValue)
}

$TotalMass = 0

foreach ($Mass in $Input) {
	while ($Mass -gt 6) {
		$Mass = Get-FuelCount($Mass)
		$TotalMass += $Mass
	}
}

$TotalMass