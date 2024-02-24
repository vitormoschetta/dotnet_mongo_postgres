#!/bin/bash

# Get token
RES_AUTH=$(curl -s -X POST -H "Content-Type: application/json" -d '{"email":"admin@admin.com","password":"123"}' http://localhost:5010/v1/auth)
echo $RES_AUTH

TOKEN=$(echo $RES_AUTH | jq -r '.data.token')
echo $TOKEN

# Delete all credit cards
RES_CREDITCARD_GET=$(curl -s -X GET -H "Content-Type: application/json" -H "Authorization: Bearer $TOKEN" http://localhost:5010/v1/credit-card)
echo $RES_CREDITCARD_GET
for i in $(echo $RES_CREDITCARD_GET | jq -r '.data[] | .id'); do
    RES_CREDITCARD_DELETE=$(curl -s -X DELETE -H "Content-Type: application/json" -H "Authorization: Bearer $TOKEN" http://localhost:5010/v1/credit-card
    echo $RES_CREDITCARD_DELETE
done

# Create credit card
RES_CREDITCARD=$(curl -s -X POST -H "Content-Type: application/json" -H "Authorization: Bearer $TOKEN" -d '{"title":"Nubank"}' http://localhost:5010/v1/credit-card)
echo $RES_CREDITCARD

CREDITCARD_ID=$(echo $RES_CREDITCARD | jq -r '.data.id')

# Create expense
RES_EXPENSE=$(curl -s -X POST -H "Content-Type: application/json" -H "Authorization: Bearer $TOKEN" -d '{"title":"Restaurante","value":60,"date":"2021-12-31","tags":["food","restaurant"]}' http://localhost:5010/v1/credit-card/$CREDITCARD_ID/expense)
echo $RES_EXPENSE

# Execute performance test
bombardier -m GET -l http://localhost:5010/v1/credit-card/$CREDITCARD_ID/expense -H "Authorization: Bearer $TOKEN" -n 10000 -r 5000

# -m: Method
# -n: Total number of requests
# -r: Number of requests per second