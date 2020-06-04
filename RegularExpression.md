# Basic topics
 
 - Anchors  -  ^ and $
    - ^The        matches any string that starts with The -> Try it!
    - end$        matches a string that ends with end
    - ^The end$   exact string match (starts and ends with The end)
    - roar        matches any string that has the text roar in it
- Quantifiers — * + ? and {}
  - abc*        matches a string that has ab followed by zero or more c -> Try it!
  - abc+        matches a string that has ab followed by one or more c
  - abc?        matches a string that has ab followed by zero or one c
  - abc{2}      matches a string that has ab followed by 2 c
  - abc{2,}     matches a string that has ab followed by 2 or more c
  - abc{2,5}    matches a string that has ab followed by 2 up to 5 c
  - a(bc)*      matches a string that has a followed by zero or more copies of the sequence bc
  - a(bc){2,5}  matches a string that has a followed by 2 up to 5 copies of the sequence bc
- OR operator — | or []
  - a(b|c)     matches a string that has a followed by b or c (and captures b or c) -> Try it!
  - a[bc]      same as previous, but without capturing b or c
- Character classes — \d \w \s and .
  - \d         matches a single character that is a digit -> Try it!
  - \w         matches a word character (alphanumeric character plus underscore) -> Try it!
  - \s         matches a whitespace character (includes tabs and line breaks)
  - .          matches any character -> Try it!
               Use the . operator carefully since often class or negated character class (which we’ll cover next) are faster and more precise.

\d, \w and \s also present their negations with \D, \W and \S respectively.
For example, \D will perform the inverse match with respect to that obtained with \d.
\D         matches a single non-digit character -> Try it!

In order to be taken literally, you must escape the characters ^.[$()|*+?{\with a backslash \ as they have special meaning.
\$\d       matches a string that has a $ before one digit -> Try it!

Notice that you can match also non-printable characters like tabs \t, new-lines \n, carriage returns \r.
Flags
We are learning how to construct a regex but forgetting a fundamental concept: flags.

A regex usually comes within this form /abc/, where the search pattern is delimited by two slash characters /. At the end we can specify a flag with these values (we can also combine them each other):
g (global) does not return after the first match, restarting the subsequent searches from the end of the previous match
m (multi-line) when enabled ^ and $ will match the start and end of a line, instead of the whole string
i (insensitive) makes the whole expression case-insensitive (for instance /aBc/i would match AbC)

- Intermediate topics
  - Grouping and capturing — ()
     - a(bc)           parentheses create a capturing group with value bc -> Try it!
     - a(?:bc)*        using ?: we disable the capturing group -> Try it!
     - a(?<foo>bc)     using ?<foo> we put a name to the group -> Try it!


# Regular expression to filter csproj files 
/(\w|\.)*(.csproj)/g

https://regexr.com/
