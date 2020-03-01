# Feature IHttpResponseBodyFeature

This new interface consolidates previous features:

- IHttpResponseFeature.Body
- IHttpSendFileFeature.SendFileAsync 
- IHttpBufferingFeature.DisableResponseBuffering

into a single interface. More info can be found in this [github issue](https://github.com/aspnet/AspNetCore/issues/12635).