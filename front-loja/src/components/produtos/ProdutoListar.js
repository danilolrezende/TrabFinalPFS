import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import api from "../../utils/api";
import Carregando from "../Carregando";
import Logout from "../acessos/Logout";

const ProdutoListar = () => {
    const [obj, setObj] = useState(null);
    const [falha, setFalha] = useState(null);
    const navigate = useNavigate();

    const carregarDados = () => {
        api.get('produtos', dados => setObj(dados), erro => setFalha(erro))
    };

    useEffect(() => {
        carregarDados();
    }, []);

    const excluir = (e, id) => {
        e.preventDefault();
        api.delete(`produtos`, id, _ => navigate(0), erro => setFalha(erro));
    }

    let mensagemFalha = null;

    if (falha) {
        mensagemFalha = (<div className="alert alert-danger">{falha}</div>);
    }

    if (!obj) {
        return (<div>
            <Carregando mensagem="" />
            {mensagemFalha}
        </div>);
    }

    const voltar = (e) => {
        e.preventDefault();
        navigate("/");
    }


    return (
        <div className="p-2">
            <div className="d-flex justify-content-between">
                <h1>Produtos</h1>
                <Logout setFalha={setFalha} />
            </div>
            {mensagemFalha}
            <div>
                <Link to={`/produtos/inserir`} className="btn btn-primary">Inserir</Link>
                <table className="table">
                    <thead>
                        <tr>
                            <th>Categoria</th>
                            <th>Nome</th>
                            <th>Preço</th>
                            <th>Quantidade</th>
                        </tr>
                    </thead>
                    <tbody>
                        {obj.map((x) => (
                            <tr key={x.id}>
                                <td>{x.categoria}</td>
                                <td>{x.nome}</td>
                                <td>{x.preco}</td>
                                <td>{x.estoque}</td>
                                <td>
                                    <Link to={`/produtos/consultar/${x.id}`} className="btn btn-secondary">Consultar</Link>
                                    <Link to={`/produtos/alterar/${x.id}`} className="btn btn-warning">Alterar</Link>
                                    <button className="btn btn-danger" onClick={e => excluir(e, x.id)}>Excluir</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
            <button className="btn btn-secondary mt-2" onClick={e => voltar(e)}>Voltar</button>
        </div>
    );
}

export default ProdutoListar;