import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import api from "../../utils/api";
import Carregando from "../Carregando";
import Logout from "../acessos/Logout";

const ClienteListar = () => {
    const [obj, setObj] = useState(null);
    const [falha, setFalha] = useState(null);
    const navigate = useNavigate();

    const carregarDados = () => {
        api.get('clientes', dados => setObj(dados), erro => setFalha(erro))
    };

    useEffect(() => {
        carregarDados();
    }, []);

    const excluir = (e, id) => {
        e.preventDefault();
        api.delete(`clientes`, id, _ => navigate(0), erro => setFalha(erro));
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
                <h1>Clientes</h1>
                <Logout setFalha={setFalha} />
            </div>
            {mensagemFalha}
            <div>
                <Link to={`/clientes/inserir`} className="btn btn-primary">Inserir</Link>
                <table className="table">
                    <thead>
                        <tr>
                            <th>Nome</th>
                            <th>Email</th>
                        </tr>
                    </thead>
                    <tbody>
                        {obj.map((x) => (
                            <tr key={x.id}>
                                <td>{x.nome}</td>
                                <td>{x.email}</td>
                                <td>
                                    <Link to={`/compras/cliente/${x.id}`} className="btn btn-info">Compras</Link>
                                    <Link to={`/clientes/consultar/${x.id}`} className="btn btn-secondary">Consultar</Link>
                                    <Link to={`/clientes/alterar/${x.id}`} className="btn btn-warning">Alterar</Link>
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

export default ClienteListar;