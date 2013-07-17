AntiXSS-Library-Simple-Speed-Test
=================================

extremely simple test to get rough estimate of AntiXSS Library speed

current   AntiXSS Library v4.2.1
date	Tue Jan 10, 2012 at 11:00 AM
status	Stable

1) 100 000 encodes = 277ms HttpUtility.HtmlEncode vs 663ms new Microsoft.Security.Application.Encoder.HtmlEncode
2) 10 000 encodes = 22ms vs 76ms
3) 1 000 encodes = 1ms vs 3ms
4) 100 encodes = 0ms vs 0ms
