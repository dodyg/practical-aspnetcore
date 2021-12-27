# URL Redirect/Rewriting (6)
  
  This section explore the dark arts of URL Rewriting

  * [Rewrite](/projects/rewrite/rewrite-1)
    
    Shows the most basic of URL rewriting which will **redirect** (returns [HTTP 302](https://en.wikipedia.org/wiki/HTTP_302)) anything to the home page "/". 
    
    If you have used routing yet, I recommend of checking out the routing examples.

  * [Rewrite - 2](/projects/rewrite/rewrite-2)
    
    **Redirect** (returns [HTTP 302](https://en.wikipedia.org/wiki/HTTP_302)) anything with an extension e.g. about-us.html or welcome.aspx to home page (/). It also shows how to capture the matched regex values.

  * [Rewrite - 3](/projects/rewrite/rewrite-3)

    **Rewrite** anything with an extension e.g. about-us.html or welcome.aspx to home page (/). It also shows how to capture the matched regex values.
    
  * [Rewrite - 4](/projects/rewrite/rewrite-4)
    
    **Permanent Redirect** (returns [HTTP 301](https://en.wikipedia.org/wiki/HTTP_301)) anything with an extension e.g. about-us.html or welcome.aspx to home page (/). It also shows how to capture the matched regex values.

  * [Rewrite - 5](/projects/rewrite/rewrite-5)
  
    Implement a custom redirect logic based on `IRule` implementation. 

    This custom redirection logic allows us to simply specify the image file names without worrying about their exact path e.g.'xx.jpg' and 'yy.png'.

  * [Rewrite - 6](/projects/rewrite/rewrite-6)
  
    Implement a custom redirect logic using lambda (similar functionality to Rewrite - 5). 

    This custom redirection logic allows us to simply specify the image file names without worrying about their exact path e.g.'xx.jpg' and 'yy.png'.

dotnet6