Feature: BookingFeature

A short summary of the feature


Scenario Outline: Book Classroom at Lancaster Royal Grammar School 
	Given The user has navigated to the webpage 
	And The user searches their Post Code <PostCode>
	And The user has selected Lancaster Royal Grammar School
	When The user books the venue for one hour on Christmas Day
	Then the booking price is correct 
	Examples:
	| PostCode |
	| LA3 1AB  |

