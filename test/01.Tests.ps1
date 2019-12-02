Describe 'Day 01a' {
	It "12 => 2" {
		.\01a.ps1 -MassValue 12 | Should -Be 2
	}
	It "14 => 2" {
		.\01a.ps1 -MassValue 14 | Should -Be 2
	}
	It "1969 => 654" {
		.\01a.ps1 -MassValue 1969 | Should -Be 654
	}
	It "100756 => 33583" {
		.\01a.ps1 -MassValue 100756 | Should -Be 33583
	}
}

Describe 'Day 01b' {
	It "14 => 2" {
		.\01b.ps1 -MassValue 14 | Should -Be 2
	}
	It "1969 => 966" {
		.\01b.ps1 -MassValue 1969 | Should -Be 966
	}
	It "100756 => 50346" {
		.\01b.ps1 -MassValue 100756 | Should -Be 50345
	}
}