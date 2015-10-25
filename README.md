# TripCalculator

A simple RESTful application to help individuals know how to evenly settle discrepant expenses made during a group trip.

## Deployment

Build and deploy using Visual Studio 2012 or later and [MVC5](http://www.asp.net/mvc/mvc5).

## Usage

Post a JSON payload containing the expense information to /api/tripexpenses.

The query should be constructed similar to the following basic data structure:
```JSON
{
  "tripMemberExpenses": [
    { "member": { "name": "Alice" }, "Expenses": [ 1.25, 1.50, 5.67, 98.41 ] },
    { "member": { "name": "Brandon" }, "Expenses": [ 49.96, 87.12, 105.78 ] },
    { "member": { "name": "Catherine" }, "Expenses": [ 1.01, 1.12, 2.23, 3.34, 5.45, 8.56 ] }
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
                ],
                "totalExpense": 106.83,
                "amountOwed": -16.97,
                "amountTransferred": 16.97,
                "amountBalance": 0
            },
            {
                "name": "Brandon",
                "expenses": [
                    49.96,
                    87.12,
                    105.78
                ],
                "totalExpense": 242.86,
                "amountOwed": 119.06,
                "amountTransferred": -119.06,
                "amountBalance": 0
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
                ],
                "totalExpense": 21.71,
                "amountOwed": -102.09,
                "amountTransferred": 102.09,
                "amountBalance": 0
            }
        ],
        "totalExpense": 371.4
    },
    "data": {
        "settlements": [
            {
                "sender": {
                    "name": "Catherine",
                    "expenses": [
                        1.01,
                        1.12,
                        2.23,
                        3.34,
                        5.45,
                        8.56
                    ],
                    "totalExpense": 21.71,
                    "amountOwed": -102.09,
                    "amountTransferred": 102.09,
                    "amountBalance": 0
                },
                "receiver": {
                    "name": "Brandon",
                    "expenses": [
                        49.96,
                        87.12,
                        105.78
                    ],
                    "totalExpense": 242.86,
                    "amountOwed": 119.06,
                    "amountTransferred": -119.06,
                    "amountBalance": 0
                },
                "amount": 102.09
            },
            {
                "sender": {
                    "name": "Alice",
                    "expenses": [
                        1.25,
                        1.5,
                        5.67,
                        98.41
                    ],
                    "totalExpense": 106.83,
                    "amountOwed": -16.97,
                    "amountTransferred": 16.97,
                    "amountBalance": 0
                },
                "receiver": {
                    "name": "Brandon",
                    "expenses": [
                        49.96,
                        87.12,
                        105.78
                    ],
                    "totalExpense": 242.86,
                    "amountOwed": 119.06,
                    "amountTransferred": -119.06,
                    "amountBalance": 0
                },
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
