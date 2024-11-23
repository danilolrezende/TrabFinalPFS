import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import api from "../../utils/api";
import Carregando from "../Carregando";

const ProdutoAlterar = () => {
    const [obj, setObj] = useState(null);
    const [falha, setFalha] = useState(null);
    const navigate = useNavigate();

    const { id } = useParams();

    useEffect(() => {
        api.get(`produtos/${id}`, dado => {
            setObj(dado);
        }, setFalha);
    }, [id]);

    const atualizarCampo = (nome, valor) => {
        let objNovo = { ...obj };
        objNovo[nome] = valor;
        setObj(objNovo);
    };

    const salvar = (e) => {
        e.preventDefault();
        api.put(`produtos/${id}`, id, obj, _ => {
            navigate(`/produtos`);
        }, setFalha);
    };

    const voltar = (e) => {
        e.preventDefault();
        navigate("/produtos");
    }

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
            <h3>Alterando Produto</h3>
            {mensagemFalha}
            <form>
                <div>
                    <label className="form-label">Categoria</label>
                    <input className="form-control" type="text"
                        onChange={e => atualizarCampo('categoria', e.target.value)} value={obj.categoria} />
                </div>
                <div>
                    <label className="form-label">Nome</label>
                    <input className="form-control" type="text"
                        onChange={e => atualizarCampo('nome', e.target.value)} value={obj.nome} />
                </div>
                <div>
                    <label className="form-label">Preço</label>
                    <input className="form-control" type="text"
                        onChange={e => atualizarCampo('preco', e.target.value)} value={obj.preco} />
                </div>
                <div>
                    <label className="form-label">Quantidade</label>
                    <input className="form-control" type="text"
                        onChange={e => atualizarCampo('estoque', e.target.value)} value={obj.estoque} />
                </div>
                <button className="btn btn-primary mt-2" onClick={e => salvar(e)}>Salvar</button>
                <button className="btn btn-secondary mt-2" onClick={e => voltar(e)}>Voltar</button>
            </form>
        </div>

    );
}

export default ProdutoAlterar;