# FoxPro-Insert
https://stackoverflow.com/questions/51844088/visual-foxpro-9-c-sharp-oledbadapter-insert-feature-is-not-available

You will [Microsoft OLE DB Provider for Visual FoxPro 9.0](https://www.microsoft.com/en-us/download/details.aspx?id=14839) to run this solution.

There is a stored procedure `newid` in `main.dbc` causing an OleDbException `Feature is not available`:

```sql
function newId
parameter thisdbf
regional keynm, newkey, cOldSelect, lDone
keynm=padr(upper(thisdbf),50)
cOldSelect=alias()
lDone=.f.
do while not lDone
    select keyvalue from main!idkeys where keyname=keynm into array akey
    if _tally=0
        insert into main!idkeys (keyname) value (keynm)
        loop
    endif
    newkey=akey+1
    update main!idkeys set keyvalue=newkey where keyname=keynm and keyvalue=akey
    if _tally=1
        lDone=.t.
    endif
enddo
if not empty(cOldSelect)
    select &cOldSelect
else
    select 0
endif
return newkey
```

TODO

[ ] Need to add the `idkeys` table used in the SP to reproduce the 
