Feature: TechQA task

@web
Scenario: 1 home page opens and loads
#	1. Go to Visma web page https://www.visma.lv/.
	Given I can open visma home page
	When home page title check

@web
Scenario: 2 and 3.1 3.3 presentation request page all marked required fields are required
#	2. Go to presentation request page by pressing# the button on the main page.
#	3. On the request form check: 
#		1) all requested fields are mandatory; 
#		3) mandatory checkbox should be pressed before submitting the request
	Given I can open visma home page
	Then accept cookies
	Then open presentation request form
	Then I fill in the following form
	| formmodel |
	| { 'Name':'name', 'Surname':'surname', 'Company':'company', 'Email':'xx@xx.xx', 'Phone':'phone', 'GDPR':false } |
	Then click form submit button
	#	GDPR checkbox is missing = 1 error, so GDPR checkbox is mandatory
	Then 1 X errors are present
	Then clear presentation request form
	Then I fill in the following form
	| formmodel |
	| { 'Name':null, 'Surname':null, 'Company':null, 'Email':null, 'Phone':null, 'GDPR':true } |
	Then click form submit button
	#	all text fields are missing = 5 errors, so 5 text fields are mandatory
	Then 5 X errors are present

@web
Scenario Outline: 3.3 Language select change page language and domain
#	2. Go to presentation request page by pressing# the button on the main page.
#	3. On the request form check: 
#		2) email field should be in format xx@xx.xx
	Given I can open visma home page
	#	Then accept cookies
	Then open presentation request form
	Then <InvalidEmail> email should not be accepted

	Examples: 
	| InvalidEmail |
	| xx@xxxx       |
	| a"b(c)d,e:f;g<h>i[j\k]l@xx.xx |
	| xx@xx@xx.xx                   |
	| xx"xx"xx@xx.xx                |
	| xx xx"xx\xx@xx.xx             |
	| xx\ xx\"xx\\xx@xx.xx          |
	# possible bug, may be rejected depending on relay local rules
	| xx..xx@xx.xx                                                             | 
	# 100% bug
	| xx@xx..xx                                                                |
	# possible bug, local part max lenght is 64, might be rejected
	| 1234567890123456789012345678901234567890123456789012345678901234x+x@xx.xx | 
	# possible bug, local part max lenght is 64, might be rejected
	| alskjnakjanv1wadfqaebbsfdfagfbnbKJANDVlvn13dcljnvzlkjbnadlkgf3cjnj@xx.xx |
	
@web
Scenario: 4 home page blog opens in a new tab
#4. Return back to the main page, scroll down and check that blog links opens new tab with different blog records. 
	Given I can open visma home page
	Then accept cookies
	Then open presentation request form
	Then go to main page
	Then scroll blog element into view
	#   PageLinkModel will be used as blog teaser model
	Then build blog teaser model
	Then open every blog record

@web
Scenario: 5 visit all social links in footer
#5. At the bottom of the page check that all social network links works and opens correctly. 
	Given I can open visma home page
	Then accept cookies
	Then scroll social link elements into view
	#   PageLinkModel will be re-used as social link element model
	Then build social links model
	Then visit every social link record

@web
Scenario Outline: 6 Language select change page language and domain
#6. Check that page changes location (url and language) when switching to another country.
	#accept cookies is skipped for some speed
	Given I can open visma home page
	#	Then accept cookies
	Then scroll language select element into view
	Then footer country select <Country>
	Then opened page has <Language> and url contains <Domain>

	#Examples are used to check all options in language select
	Examples: 
	| Country     | Language | Domain   |
	| Denmark     | da       | .dk      |
	| Finland     | fi       | .fi      |
	| Latvia      | lv       | .lv      |
	| Lithuania   | lt       | .lt      |
	| Netherlands | nl       | nl.      |
	| Norway      | nb       | .no      |
	| Romania     | ro       | .ro      |
	| Sweden      | sv       | .se      |
	| UK          | en       | .co.uk   |
	| Visma.com   | en       | .com     |
