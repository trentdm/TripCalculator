# TripCalculator

A simple RESTful application to help individuals know how to evenly settle discrepant expenses made during a group trip.

## Deployment

Build and deploy using Visual Studio 2012 or later and [MVC5](http://www.asp.net/mvc/mvc5).

## Usage

Post a JSON payload containing the expense information to /api/tripexpenses.

The query should be constructed similar to the following basic data structure:
```JSON
{
  "TripMembers": [
    { "name": "Alice", "Expenses": [ 1.25, 1.50, 5.67, 98.41 ] },
    { "name": "Brandon", "Expenses": [ 49.96, 87.12, 105.78 ] },
    { "name": "Catherine", "Expenses": [ 1.01, 1.12, 2.23, 3.34, 5.45, 8.56 ] }
  ]
}
```

The response will be constructed similar to the following:
```JSON
{
    "query": {
        "tripMembers": [
            {
                "name": "Alice",
                "expenses": [
                    1.25,
                    1.5,
                    5.67,
                    98.41
                ]
            },
            {
                "name": "Brandon",
                "expenses": [
                    49.96,
                    87.12,
                    105.78
                ]
            },
            {
                "name": "Catherine",
                "expenses": [
                    1.01,
                    1.12,
                    2.23,
                    3.34,
                    5.45,
                    8.56
                ]
            }
        ]
    },
    "data": {
        "settlements": [
            {
                "senderName": "Catherine",
                "receiverName": "Brandon",
                "amount": 102.09
            },
            {
                "senderName": "Alice",
                "receiverName": "Brandon",
                "amount": 16.97
            }
        ]
    }
}
```

## Requirements

Requires Visual Studio 2012 or later and [MVC5](http://www.asp.net/mvc/mvc5).

Trip Calculator was built in Visual Studio 2015, so expect best support from VS2015 or later.

## License

[MIT](./LICENSE).
