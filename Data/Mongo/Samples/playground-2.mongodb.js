// 01. busca todos os usuários
use('pfmdb');
db.users.find({}).pretty();


// 02. busca todos os cartões de crédito
use('pfmdb');
db.credit_cards.find({}).pretty();


// 03. busca todas as despesas
use('pfmdb');
db.expenses.find({}).pretty();


// 04. busca todas as despesas com tag 'compras'
use('pfmdb');
db.expenses.find({
    tags: 'compras', 
}).pretty();


// 05. busca todas as despesas com tag 'compras' e valor maior que 50
use('pfmdb');
db.expenses.find({
  tags: 'compras',
  value: {
      $gt: 50,
  },
}).pretty();


// 06. busca todas as despesas entre os dias 01/01/2021 e 02/01/2021
use('pfmdb');
db.expenses.find({
    date: {
        $gte: '2021-01-01',
        $lte: '2021-01-02',
    },
}).pretty();


// 07. busca todas as despesas do usuario johndoe@gmail.com
use('pfmdb');
var user = db.users.findOne({email: 'johndoe@gmail.com'});
var creditCards = db.credit_cards.find({user_id: user._id}).toArray();
var creditCardIds = creditCards.map(function (creditCard) {
    return creditCard._id;
});
db.expenses.find({
    credit_card_id: {
        $in: creditCardIds,
    },
}).pretty();


// 08. mesma consulta 07, mas excluindo os campos _id, credit_card_id e tags do resultado
// nota: no mongodb usamos os valores 0 para excluir um campo do resultado e 1 para incluir um campo no resultado
use('pfmdb');
var user = db.users.findOne({email: 'johndoe@gmail.com'});
var creditCards = db.credit_cards.find({user_id: user._id}).toArray();
var creditCardIds = creditCards.map(function (creditCard) {
    return creditCard._id;
});
db.expenses.find(
    {
        credit_card_id:{$in: creditCardIds}
    }, 
    {
        _id: 0, credit_card_id: 0, tags: 0
    }
).pretty();


// 09. mesma consulta 07, mas em uma única query
// nota: no mongodb usamos as claues $lookup, $unwind e $match para fazer joins
// nota2: teremos um objeto credit_card dentro de cada despesa
use('pfmdb');
db.expenses.aggregate([
    {
        $lookup: {
            from: 'credit_cards',
            localField: 'credit_card_id',
            foreignField: '_id',
            as: 'credit_card',
        },
    },
    {
        $unwind: '$credit_card',
    },
    {
        $match: {
            'credit_card.user_id': 'johndoe@gmail.com',
        },
    }
]).pretty();


// 10. mesma consulta 09, mas removendo o objeto credit_card do resultado
// nota: teremos o mesmo resultado da consulta 07
use('pfmdb');
db.expenses.aggregate([
    {
        $lookup: {
            from: 'credit_cards',
            localField: 'credit_card_id',
            foreignField: '_id',
            as: 'credit_card',
        },
    },
    {
        $unwind: '$credit_card',
    },
    {
        $match: {
            'credit_card.user_id': 'johndoe@gmail.com',
        },
    },
    {
        $project: {
            credit_card: 0,
        },
    },
]).pretty();


// 11. busca todas as despesas do usuario johndoe@gmail.com e do cartão de crédito Nubank
use('pfmdb');
db.expenses.aggregate([
    {
        $lookup: {
            from: 'credit_cards',
            localField: 'credit_card_id',
            foreignField: '_id',
            as: 'credit_card',
        },
    },
    {
        $unwind: '$credit_card',
    },
    {
        $match: {
            'credit_card.user_id': 'johndoe@gmail.com',
            'credit_card.title': 'Nubank',
        },
    },
    {
        $project: {
            credit_card: 0,
        },
    },
]).pretty();


// 12. busca todas as tags (usando distinct) do usuario johndoe@gmail.com
use('pfmdb');
db.expenses.aggregate([
    {
        $lookup: {
            from: 'credit_cards',
            localField: 'credit_card_id',
            foreignField: '_id',
            as: 'credit_card',
        },
    },
    {
        $unwind: '$credit_card',
    },
    {
        $match: {
            'credit_card.user_id': 'johndoe@gmail.com'
        }
    },
    {
        $unwind: '$tags',
    },
    {
        $group: {
          _id: 0,
          allTags: {
            $addToSet: '$tags',
          }
        }
    },
]).pretty();


// uso da clausula $project: 
// 0 para excluir um campo do resultado
// 1 para incluir um campo no resultado
// não pode usar os dois ao mesmo tempo, com exceção do _id


