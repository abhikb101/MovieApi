# Movie API Documentation

# REST API

The REST API to the movie app is described below.

## Get Actors

### Request

`GET /api/Actors`

### Response

    HTTP/1.1 200 OK
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 200 OK
    Connection: close
    Content-Type: application/json
    Content-Length: 2

    [
        {
        "name": "string",
        "bio": "string",
        "dateOfBirth": "2021-11-15T16:24:36.352Z",
        "gender": "string",
        "actorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        }
    ]

## Create Actor

### Request

`POST /api/Actors`

    Request body - appication/json-patch+json
    {
      "name": "string",
      "bio": "string",
      "dateOfBirth": "2021-11-15T16:25:40.438Z",
      "gender": "string",
    }

### Response

    HTTP/1.1 201 Created
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 201 Created
    Connection: close
    Content-Type: application/json
    Location: /thing/1
    Content-Length: 36

    {
        "name": "string",
        "bio": "string",
        "dateOfBirth": "2021-11-15T16:24:36.352Z",
        "gender": "string",
        "actorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    }

## Get Actor

### Request

`GET api/Actor/id`


### Response

    HTTP/1.1 200 OK
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 200 OK
    Connection: close
    Content-Type: application/json
    Content-Length: 36

    {
        "name": "string",
        "bio": "string",
        "dateOfBirth": "2021-11-15T16:24:36.352Z",
        "gender": "string",
        "actorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    }

## Update Producers

### Request

`PUT /api/Actors/id`

    Request body - appication/json-patch+json
    {
      "name": "string",
      "bio": "string",
      "dateOfBirth": "2021-11-15T16:25:40.438Z",
      "gender": "string",
    }


### Response

    HTTP/1.1 201 Created
    Date: Thu, 24 Feb 2011 12:36:31 GMT
    Status: 201 Created
    Connection: close
    Content-Type: application/json
    Location: /thing/2
    Content-Length: 35

## Delete Actor

### Request

`DELETE api/Actors/id`

### Response

    HTTP/1.1 204 No Content
    Date: Thu, 24 Feb 2011 12:36:32 GMT
    Status: 204 No Content
    Connection: close

## Get Producers

### Request

`GET /api/Producers`

### Response

    HTTP/1.1 200 OK
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 200 OK
    Connection: close
    Content-Type: application/json
    Content-Length: 2

    [
        {
            "name": "string",
            "bio": "string",
            "dateOfBirth": "2021-11-15T16:34:46.696Z",
            "gender": "string",
            "producerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "company": "string"
        }
    ]

## Create Producers

### Request

`POST /api/Producers`

    Request body - appication/json-patch+json
    {
      "name": "string",
      "bio": "string",
      "dateOfBirth": "2021-11-15T16:25:40.438Z",
      "gender": "string",
    }

### Response

    HTTP/1.1 201 Created
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 201 Created
    Connection: close
    Content-Type: application/json
    Location: /thing/1
    Content-Length: 36

        {
            "name": "string",
            "bio": "string",
            "dateOfBirth": "2021-11-15T16:34:46.696Z",
            "gender": "string",
            "producerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "company": "string"
        }

## Get Producer

### Request

`GET api/Producers/id`


### Response

    HTTP/1.1 200 OK
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 200 OK
    Connection: close
    Content-Type: application/json
    Content-Length: 36
    
    {
      "name": "string",
      "bio": "string",
      "dateOfBirth": "2021-11-15T16:34:46.696Z",
      "gender": "string",
      "producerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "company": "string"
    }
    
## Update Actor

### Request

`PUT /api/Producers/id`

    Request body - appication/json-patch+json
    {
      "name": "string",
      "bio": "string",
      "dateOfBirth": "2021-11-15T16:34:46.696Z",
      "gender": "string",
      "producerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "company": "string"
    }


### Response

    HTTP/1.1 201 Created
    Date: Thu, 24 Feb 2011 12:36:31 GMT
    Status: 201 Created
    Connection: close
    Content-Type: application/json
    Location: /thing/2
    Content-Length: 35

## Delete Producer

### Request

`DELETE api/Producers/id`

### Response

    HTTP/1.1 204 No Content
    Date: Thu, 24 Feb 2011 12:36:32 GMT
    Status: 204 No Content
    Connection: close

## Get Movies

### Request

`GET /api/Movies`

### Response

    HTTP/1.1 200 OK
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 200 OK
    Connection: close
    Content-Type: application/json
    Content-Length: 2

    [
      {
        "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string",
        "plot": "string",
        "releaseDate": "2021-11-15T16:43:28.659Z",
        "actors": [
          {
            "name": "string",
            "bio": "string",
            "dateOfBirth": "2021-11-15T16:43:28.659Z",
            "gender": "string",
            "actorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
          }
        ],
        "producers": [
          {
            "name": "string",
            "bio": "string",
            "dateOfBirth": "2021-11-15T16:43:28.659Z",
            "gender": "string",
            "producerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "company": "string"
          }
        ]
      }
    ]

## Create Movie

### Request

`POST /api/Movies`

    Request body - appication/json-patch+json
    {
      "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "string",
      "plot": "string",
      "releaseDate": "2021-11-15T16:44:02.372Z",
    }

### Response

    HTTP/1.1 201 Created
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 201 Created
    Connection: close
    Content-Type: application/json
    Location: /thing/1
    Content-Length: 36

    {
      "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "string",
      "plot": "string",
      "releaseDate": "2021-11-15T16:44:02.372Z",
      "actors": [],
      "producers":[],
    }


## Get Movie

### Request

`GET api/Movie/id`


### Response

    HTTP/1.1 200 OK
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 200 OK
    Connection: close
    Content-Type: application/json
    Content-Length: 36
    
      {
        "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string",
        "plot": "string",
        "releaseDate": "2021-11-15T16:43:28.659Z",
        "actors": [
          {
            "name": "string",
            "bio": "string",
            "dateOfBirth": "2021-11-15T16:43:28.659Z",
            "gender": "string",
            "actorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
          }
        ],
        "producers": [
          {
            "name": "string",
            "bio": "string",
            "dateOfBirth": "2021-11-15T16:43:28.659Z",
            "gender": "string",
            "producerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "company": "string"
          }
        ]
      }
    
## Update Movie

### Request

`PUT /api/Movie/id`

    Request body - appication/json-patch+json
      {
        "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string",
        "plot": "string",
        "releaseDate": "2021-11-15T16:43:28.659Z",
      }


### Response

    HTTP/1.1 201 Created
    Date: Thu, 24 Feb 2011 12:36:31 GMT
    Status: 201 Created
    Connection: close
    Content-Type: application/json
    Location: /thing/2
    Content-Length: 35

## Delete Movie

### Request

`DELETE api/Movies/id`

### Response

    HTTP/1.1 204 No Content
    Date: Thu, 24 Feb 2011 12:36:32 GMT
    Status: 204 No Content
    Connection: close

## Create CastAndCrew

### Request
actorid = actor or producer id
role - 0:actor, 1:producer

`POST /api/CastAndCrews?movieid=movieid&actorid=actorid&role=role`

    Request body - appication/json-patch+json
    {
      "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "string",
      "plot": "string",
      "releaseDate": "2021-11-15T16:44:02.372Z",
    }


## Delete CastAndCrew

### Request 
actorid = actor or producer id
role - 0:actor, 1:producer

`DELETE api/CastAndCrews/actorid?movieid=movieid&role=role`

### Response

    HTTP/1.1 204 No Content
    Date: Thu, 24 Feb 2011 12:36:32 GMT
    Status: 204 No Content
    Connection: close


