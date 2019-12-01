param (
	[string]$InputFile,
	[int]$MassValue
)

function Get-FuelCount([int]$Mass) {
	return [Math]::Max([Math]::floor($Mass / 3) - 2, 0)
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
	$TotalMass += Get-FuelCount($Mass)
}

$TotalMass