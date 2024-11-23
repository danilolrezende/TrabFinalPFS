import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../utils/api";

const ProdutoInserir = () => {
    const [obj, setObj] = useState([
        {  categoria: 0, nome: "", preco: 0, estoque: 0 }
    ]);
    const [falha, setFalha] = useState(null);

    const navigate = useNavigate();

    const salvar = e => {
        e.preventDefault();
        api.post(`produtos`, obj, resp => {
            navigate(`/produtos`);
        }, setFalha);
    };

    const voltar = (e) => {
        e.preventDefault();
        navigate("/produtos");
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
            <h3>Inserindo Produto</h3>
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

export default ProdutoInserir;