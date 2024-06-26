# JWT Validation API

API used for validating JWT token signed with RSA 256 encrypted x.509 certificate.
It is using .pem file containing the public key to validate the JWT token

## Swagger Documentation

```python
https://developmentapi.deldiosmotorclubadmin.com/swagger/index.html
```

Note: JWT Bearer Token is required for the testing on swagger which is given at the end of the file


## Languages and Frameworks
C# 12

.NET Core 8.0

## Packages
Microsoft.AspNetCore.Authentication.JwtBearer (8.0.4)

Swashbuckle.AspNetCore (6.5.0)

System.IdentityModel.Tokens.Jwt (7.5.1)

Microsoft.AspNetCore.Mvc.Testing (8.0.4)

Microsoft.EntityFrameworkCore.InMemory (8.0.4) 

Microsoft.NET.Test.Sdk (17.9.0)

xunit (2.7.1)

xunit.runner.visualstudio (2.5.8)


## Installation

To run the project on machine, Git Clone the project

Note: Required packages are already installed in the project 

In case of missing sdk please refer to the Microsoft website on the following link and download .Net 8.0


```bash
https://dotnet.microsoft.com/en-us/download/visual-studio-sdks
```
In case of any missing project dependencies, please run the following command in the project directory

```bash
dotnet restore
```


## Testing on swagger

Paste these token in the swagger authorization and click on the try

```python
validToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsIng1dSI6Imh0dHBzOi8vZG1jaWNvbnNkZXZlbG9wbWVudC5kZWxkaW9zbW90b3JjbHViYWRtaW4uY29tL0ljb25zL2VuYWJsZWJhbmtpbmd0ZXN0LWNlcnQtcHVibGljLnBlbSJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6Ik0gVGFsaGEgQXJzaGFkIiwiaWF0IjoxNTc4NzcwMDAwLCJleHAiOjE3Nzg3NTIwMDB9.dzjDBpeTKqW7FGYj9Utz0m_1iak6TkgyFEGL1j6AfpaWYlc46d_ROUhtnp5TsZc5XPBaW1fGoZ5vrG5jf080bJm0MUhBMsMoUHmUnU5PFhkpyZhY1ZXxl6ic-wMWeKU4o4OBgDroQS8eme1bht6MmodZrMWTWyfevw_rprzvz1Yv7qvZP7yElaXOFBNdpODP3vLMve_Pq9HTkfk3VBpW-My8wuOEIy9ZbrXD84Yhib72pFNHa7p_m_fgP947qprh-TgeZ9PViU3LHYCPlrumMUv4U69wPTRVpgoyq5hnqlDbPM-FaxhR7Jg_FQ9kKofHrj6dWWMWOZIbeCgIoS0uIg"

expiredToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsIng1dSI6Imh0dHBzOi8vZG1jaWNvbnNkZXZlbG9wbWVudC5kZWxkaW9zbW90b3JjbHViYWRtaW4uY29tL0ljb25zL2VuYWJsZWJhbmtpbmd0ZXN0LWNlcnQtcHVibGljLnBlbSJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6Ik0gVGFsaGEgQXJzaGFkIiwiaWF0IjoxMjc4NzcwMDAwLCJleHAiOjEyNzg3NTIwMDB9.ThtD6C-xq6SbZV9WdgdsCNmsLtfnm2xxJgVrOxPQKNjhTQ3sLhBRX-NWzO4T4_7Ttg0Hm1sSZhu-IoUDqbuFR-1ijeytr2C4KIe_ZsSKkX5T27_AafXZgNVQ2oU6HD4ynGLMyiTwzvnWyIVcHfVmcL0sI2MNhyt4fxtCirv-pQhB6jLVjgAMcOr3IFsCeMkmmPBUBna2h4Q8n0rXIUrZdP0a71rkc-IoSORCgu1ex68xV5yPVl6hne9fAc6t6yoYjJMfbvzpo79Bq__qeQY_6hi7zL44cL34Fz_5pc1HTuQxYo7wxZU548l8x5WnreIVdQGd9rreMgDeLOIor7fvgw"

```

## Test Cases
Test Cases can be run to check some possible scenarios of the API

Steps

1- Git Clone enableTestAPI -> JWTValidationTester

2- Navigate to the directory

3- Open your terminal in the directory

2- run following command

```python
dotnet test
```
Report of all test could be seen on terminal

## Docker
Follow these steps to run docker file

Steps

1- Git Clone enableTestAPI -> JWTValidationAPI

2- Navigate to the directory

3- Open your terminal in the directory

2- run following command

Build the Image
```python
docker build -t your-image-name .
```

Run a container based on the above created image 

```python
docker run -d -p 8080:8080 -p 8081:8081 your-image-name
```

verify the status

```python
docker ps
```

You can access swagger end point at this url

```python
http://localhost:8080/swagger/index.html
```

## JWT Token Information

JWT token for this project was created with a public and private key generated by as RSA 256 x.509 certificate from the following website

```python
https://jwt.io/
```

## Certificate Information
Openssl was used in the test project to create the x509 certificate

RSA 256 encrypted x509 public key certificate could be found from the following URL

```python
https://dmciconsdevelopment.deldiosmotorclubadmin.com/Icons/enablebankingtest-cert-public.pem
```

## License

[MIT](https://choosealicense.com/licenses/mit/)
