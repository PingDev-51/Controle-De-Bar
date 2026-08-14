
using Microsoft.EntityFrameworkCore;
using FizzWare.NBuilder;



namespace eAgenda.Testes.Integracao.Compartilhado.Orm;

public abstract class RepositorioBaseEmOrmTests
{
    // protected EAgendaDbContext dbContext = null!;
    // protected RepositorioCategoriaEmOrm repositorioCategoria = null!;


    //mudar na hora de desenvolver vou deixar um como base, dps so substiruir e continuar codando

    [TestInitialize]
    public void InicializarContexto()
    {

        // Categoria
        // repositorioCategoria = new RepositorioCategoriaEmOrm(dbContext);

        // BuilderSetup.SetCreatePersistenceMethod<Categoria>(repositorioCategoria.Cadastrar);
        // BuilderSetup.SetCreatePersistenceMethod<IList<Categoria>>((categorias) =>
        // {
        //     foreach (Categoria d in categorias)
        //         repositorioCategoria.Cadastrar(d);
        // });

        // // Compromisso
        // repositorioCompromisso = new RepositorioCompromissoEmOrm(dbContext);

        // BuilderSetup.SetCreatePersistenceMethod<Compromisso>(repositorioCompromisso.Cadastrar);
        // BuilderSetup.SetCreatePersistenceMethod<IList<Compromisso>>((compromisso) =>
        // {
        //     foreach (Compromisso c in compromisso)
        //         repositorioCompromisso.Cadastrar(c);
        // });

        // // Questao
        // repositorioContato = new RepositorioContatoEmOrm(dbContext);

        // BuilderSetup.SetCreatePersistenceMethod<Contato>(repositorioContato.Cadastrar);
        // BuilderSetup.SetCreatePersistenceMethod<IList<Contato>>((contatos) =>
        // {
        //     foreach (Contato c in contatos)
        //         repositorioContato.Cadastrar(c);
        // });

        // // Prova
        // repositorioDespesa = new RepositorioDespesaEmOrm(dbContext);

        // BuilderSetup.SetCreatePersistenceMethod<Despesa>(repositorioDespesa.Cadastrar);
        // BuilderSetup.SetCreatePersistenceMethod<IList<Despesa>>((despesas) =>
        // {
        //     foreach (Despesa d in despesas)
        //         repositorioDespesa.Cadastrar(d);
        // });


        // // Tarefa
        // repositorioTarefa = new RepositorioTarefaEmOrm(dbContext);

        // BuilderSetup.SetCreatePersistenceMethod<Tarefa>(repositorioTarefa.Cadastrar);
        // BuilderSetup.SetCreatePersistenceMethod<IList<Tarefa>>((tarefas) =>
        // {
        //     foreach (Tarefa t in tarefas)
        //         repositorioTarefa.Cadastrar(t);
        // });
    }

    [TestCleanup]
    public void DescartarContexto()
    {
        // dbContext.Dispose();
    }
}
