Describe 'Day 02a' {
	It "1,0,0,0,99 => 2,0,0,0,99" {
		.\02a.ps1 -Program "1,0,0,0,99" -DontAdd1202 -OutputFullProgram | Should -Be "2,0,0,0,99"
	}
	It "2,3,0,3,99 => 2,3,0,6,99" {
		.\02a.ps1 -Program "2,3,0,3,99" -DontAdd1202 -OutputFullProgram | Should -Be "2,3,0,6,99"
	}
	It "2,4,4,5,99,0 => 2,4,4,5,99,9801" {
		.\02a.ps1 -Program "2,4,4,5,99,0" -DontAdd1202 -OutputFullProgram | Should -Be "2,4,4,5,99,9801"
	}
	It "1,1,1,4,99,5,6,0,99 => 30,1,1,4,2,5,6,0,99" {
		.\02a.ps1 -Program "1,1,1,4,99,5,6,0,99" -DontAdd1202 -OutputFullProgram | Should -Be "30,1,1,4,2,5,6,0,99"
	}
}

Describe 'Day 02b' {
	It "I don't have any tests" {
		$True | Should -Be $True
	}
}