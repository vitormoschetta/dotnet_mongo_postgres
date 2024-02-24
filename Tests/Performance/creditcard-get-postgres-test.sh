#!/bin/bash

RES_AUTH=$(curl -s -X POST -H "Content-Type: application/json" -d '{"email":"admin@admin.com","password":"123"}' http://localhost:5020/v1/auth)
echo $RES_AUTH

TOKEN=$(echo $RES_AUTH | jq -r '.data.token')
echo $TOKEN

RES_CREATE_CREDITCARD=$(curl -s -X POST -H "Content-Type: application/json" -H "Authorization: Bearer $TOKEN" -d '{"title":"Nubank"}' http://localhost:5020/v1/credit-card)
echo $RES_CREATE_CREDITCARD

bombardier -m GET -l http://localhost:5020/v1/credit-card -H "Authorization: Bearer $TOKEN" -n 50000 -r 10000

# -m: Method
# -n: Total number of requests
# -r: Number of requests per second