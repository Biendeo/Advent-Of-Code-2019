param (
	[string]$InputFile,
	[string]$Program,
	[switch]$DontAdd1202,
	[switch]$OutputFullProgram
)

if (!$InputFile -and !$Program) {
	Write-Output "Please specify either a program or a file that contains the program"
	exit
}

if ($InputFile) {
	$Input = [int[]]((Get-Content $InputFile).Split(",") | ForEach-Object { [int]$_ })
} else {
	$Input = [int[]]($Program.Split(",") | ForEach-Object { [int]$_ })
}

if (!$DontAdd1202) {
	$Input[1] = 12
	$Input[2] = 2
}

$CurrentIndex = 0

while ($CurrentIndex -lt $Input.Count) {
	if ($Input[$CurrentIndex] -eq 1) {
		$Num1 = $Input[$Input[$CurrentIndex + 1]]
		$Num2 = $Input[$Input[$CurrentIndex + 2]]
		$Input[$Input[$CurrentIndex + 3]] = $Num1 + $Num2
		$CurrentIndex += 4
	} elseif ($Input[$CurrentIndex] -eq 2) {
		$Num1 = $Input[$Input[$CurrentIndex + 1]]
		$Num2 = $Input[$Input[$CurrentIndex + 2]]
		$Input[$Input[$CurrentIndex + 3]] = $Num1 * $Num2
		$CurrentIndex += 4
	} elseif ($Input[$CurrentIndex] -eq 99) {
		break
	} else {
		Write-Error Program encountered an opcode of $Input[$CurrentIndex]
	}
}

if (!$OutputFullProgram) {
	$Input[0]
} else {
	$Input -join ","
}