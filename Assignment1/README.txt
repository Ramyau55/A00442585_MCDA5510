This is read me file which provides the details about c# Assignment 1.

Results/Output.

1. The Output (result) is located under "A00442585_MCDA5510\Assignment1\Output" folder.

2. All the Information(Execution Time,Skipped row and processed rows) and all the exception(checked exception) are added to the Log.txt which is located under "A00442585_MCDA5510\Assignment1\Log".

3. If the file is not having the proper columns then I am ignoring the file. So the rows in the ignored file will not be counted in neither processed records nor skipped records. The igored file details are logged in Log.txt

4. If file is not ".csv", then ignoring the file and logging the file info in the Log.txt.

5. Calculating the total execution time and logging the information in Log.txt

6. output.csv -  Headers are ignored here. Only the rows with all the mandatory values are considered valid row and counted as processed row. if any one of the following field is null or blank, I am skipping the row. Also added few additional field level validation for few fields.If those additional validations are not met , I am skipping that particular row as well and will be counted as skipped row.

firstName - cannot be null, can have only alphabet and space
lastName - cannot be null, can have only alphabet and space
streetNumber - cannot be null
street - cannot be null
city - cannot be null
province - cannot be null, can have only alphabet and space
postalCode- cannot be null
country - cannot be null, can have only alphabet and space
phoneNumber - cannot be null
emailAddress - cannot be null, Should have @ and domain with 2 or 3 letters and few other basic validation.


7. Ignoring the header in all files while reading so that the headers will not get counted in both Processed records and skipped records

8. Calculating the "file date" by using the folder structure of sample data. The corresponding date is updated for each record in the output.csv



