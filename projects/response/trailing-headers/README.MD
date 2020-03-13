# Trailing Headers

This example shows how to issue trailing HTTP headers. Normal HTTP headers must be issued before body of the HTTP Response starts being written. Trailing headers allows you to issue headers after the HTTP body has been written. 

Open the sample at https://localhost:5000 and open your browser dev tools. You will be able to see the trailer header being issued but not its content.