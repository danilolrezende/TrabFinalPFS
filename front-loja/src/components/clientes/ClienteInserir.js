import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../utils/api";

const ClienteInserir = () => {
    const [obj, setObj] = useState([
        {  nome: "", email: "" }
    ]);
    const [falha, setFalha] = useState(null);

    const navigate = useNavigate();

    const salvar = e => {
        e.preventDefault();
        api.post(`clientes`, obj, resp => {
            navigate(`/clientes`);
        }, setFalha);
    };

    const voltar = (e) => {
        e.preventDefault();
        navigate("/clientes");
    }

    const atualizarCampo = (nome, valor) => {
        setObj({...obj, [nome]: valor });
    };

    let mensagemFalha = null;

    if (falha) {
        mensagemFalha = (<div className="alert alert-danger">{falha}</div>);
        setTimeout(() => {
            setFalha(null);
        }, 10000);
    }

    return (
        <div className="p-2">
            <h3>Inserindo Cliente</h3>
            {mensagemFalha}
            <form>
                <div>
                    <label className="form-label">Nome</label>
                    <input className="form-control" type="text"
                        onChange={e => atualizarCampo('nome', e.target.value)} value={obj.nome} />
                </div>
                <div>
                    <label className="form-label">Email</label>
                    <input className="form-control" type="email"
                        onChange={e => atualizarCampo('email', e.target.value)} value={obj.email} />
                </div>
                <button className="btn btn-primary mt-2" onClick={e => salvar(e)}>Salvar</button>
                <button className="btn btn-secondary mt-2" onClick={e => voltar(e)}>Voltar</button>
            </form>
        </div>
        
    );  
}

export default ClienteInserir;