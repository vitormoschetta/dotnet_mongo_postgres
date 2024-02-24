// comando para adicionar uma tag a um documento
use('pfmdb');
db.expenses.updateOne(
    { title: 'Uber' },
    { $push: { tags: 'cristiane' } }
);

// comando para remover uma tag de um documento
use('pfmdb');
db.expenses.updateOne(
    { title: 'Uber' },
    { $pull: { tags: 'cristiane' } }
);

// comando para adicionar uma tag a todos os documentos
use('pfmdb');
db.expenses.updateMany(
    {},
    { $push: { tags: 'cristiane' } }
);

// comando para remover uma tag de todos os documentos
use('pfmdb');
db.expenses.updateMany(
    {},
    { $pull: { tags: 'cristiane' } }
);

// comando para alterar o title de uma despesa de Uber para Uber Eats
use('pfmdb');
db.expenses.updateOne(
    { title: 'Uber' },
    { $set: { title: 'Uber Eats' } }
);

// comando para alterar o title de todas as despesas de Uber para Uber Eats
use('pfmdb');
db.expenses.updateMany(
    { title: 'Uber' },
    { $set: { title: 'Uber Eats' } }
);

// comando para adicinar uma tag a uma despesa "cristiane" em todas as despesas vinculadas ao cartão de crédito Nubank
use('pfmdb');
var creditCard = db.credit_cards.findOne({ title: 'Nubank' });
db.expenses.updateMany(
    { credit_card_id: creditCard._id },
    { $push: { tags: 'cristiane' } }
);