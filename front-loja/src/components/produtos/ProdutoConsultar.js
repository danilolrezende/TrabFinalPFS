import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import api from "../../utils/api";
import Carregando from "../Carregando";

const ProdutoConsultar = () => {
    const [obj, setObj] = useState(null);
    const [falha, setFalha] = useState(null);
    const { id } = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        api.get(`produtos/${id}`, dado => {
            setObj(dado);
        }, setFalha);
    }, [id]);

    const voltar = (e) => {
        e.preventDefault();
        navigate("/produtos");
    };

    let mensagemFalha = null;

    if (falha) {
        mensagemFalha = (<div className="alert alert-danger">{falha}</div>);
        setTimeout(() => {
            setFalha(null);
        }, 10000);
    }

    if (!obj) {
        return (<div>
            <Carregando mensagem="" />
            {mensagemFalha}
        </div>);
    }


    return (
        <div className="p-2">
            <h2>Detalhes do Produto</h2>
            {mensagemFalha}
            <form>
                <div>
                    <label className="form-label">Categoria</label>
                    <input className="form-control" disabled={true} type="text" value={obj.categoria} />
                </div>
                <div>
                    <label className="form-label">Nome</label>
                    <input className="form-control" disabled={true} type="text" value={obj.nome} />
                </div>
                <div>
                    <label className="form-label">Preco</label>
                    <input className="form-control" disabled={true} type="text" value={obj.preco} />
                </div>
                <div>
                    <label className="form-label">Quantidade</label>
                    <input className="form-control" disabled={true} type="text" value={obj.estoque} />
                </div>
                <button className="btn btn-secondary mt-2" onClick={e => voltar(e)}>Voltar</button>
            </form>
        </div>
    );
}

export default ProdutoConsultar;  