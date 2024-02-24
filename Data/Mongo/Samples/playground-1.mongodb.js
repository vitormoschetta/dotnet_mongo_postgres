use('pfmdb');
db.users.insertMany([
    {
        _id: 'johndoe@gmail.com',
        name: 'John Doe',
        email: 'johndoe@gmail.com',
    },
    {
        _id: 'vithor@gmail.com',
        name: 'ViThor',
        email: 'vithor@gmail.com',
    }
]);

db.credit_cards.insertMany([
    {
        _id: 1,
        title: 'Nubank',
        limit: 2000,
        user_id: 'johndoe@gmail.com',
    },
    {
        _id: 2,
        title: 'Mercado Pago',
        limit: 1000,
        user_id: 'johndoe@gmail.com',
    },
    {
        _id: 3,
        title: 'Santander',
        limit: 3000,
        user_id: 'vithor@gmail.com'
    }
]);

db.expenses.insertMany([
    {
        _id: 1,
        title: 'Supermercado',
        value: 100,
        date: '2021-01-01',
        credit_card_id: 1,
        tags: ['compras', 'mercado'],
    },
    {
        _id: 2,
        title: 'Restaurante',
        value: 50,
        date: '2021-01-02',
        credit_card_id: 1,
        tags: ['compras', 'restaurante'],
    },
    {
        _id: 3,
        title: 'Uber',
        value: 20,
        date: '2021-01-03',
        credit_card_id: 2,
        tags: ['transporte'],
    },
    {
        _id: 4,
        title: 'Netflix',
        value: 15,
        date: '2021-01-04',
        credit_card_id: 3,
        tags: ['entretenimento'],
    },
    {
        _id: 5,
        title: 'Amazon',
        value: 200,
        date: '2021-01-05',
        credit_card_id: 3,
        tags: ['compras'],
    },
]);

