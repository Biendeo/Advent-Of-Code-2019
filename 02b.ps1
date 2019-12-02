param (
	[string]$InputFile,
	[string]$Program
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

$OriginalInput = $Input.Clone()

$Noun = -1
$Verb = 0

do {
	$Noun += 1
	if ($Noun -eq 100) {
		$Noun = 0
		$Verb += 1
	}

	$Input = $OriginalInput.Clone()
	$Input[1] = $Noun
	$Input[2] = $Verb
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
} while ($Input[0] -ne 19690720)

100 * $Noun + $Verb