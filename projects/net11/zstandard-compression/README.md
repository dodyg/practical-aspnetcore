# Zstandard response compression and request decompression

This sample demonstrates [Zstandard (zstd)](https://facebook.github.io/zstd/) support added to the response-compression and request-decompression middleware. 

Verify the response compression (check `content-encoding`):

```windows
curl.exe -s -o NUL -D - -H "Accept-Encoding: zstd" http://localhost:5000/
```

Verify request decompression by sending a zstd-compressed body:

In Linux

Make sure you have zstd
```
sudo apt install zstd
```

```bash
echo -n "Hello, zstd!" | zstd -o payload.zst
curl -H "Content-Encoding: zstd" --data-binary @payload.zst http://localhost:5000/echo
```
