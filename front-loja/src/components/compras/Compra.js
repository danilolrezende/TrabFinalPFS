import React, { useState, useEffect } from "react";
import api from "../../utils/api";
import { useNavigate } from "react-router-dom";

const CompraPage = () => {
    const [clientes, setClientes] = useState([]);
    const [produtos, setProdutos] = useState([]);
    const [clienteId, setClienteId] = useState("");
    const [produtoId, setProdutoId] = useState("");
    const [quantidade, setQuantidade] = useState(1);
    const [mensagemErro, setMensagemErro] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        // Carregar clientes e produtos
        api.get("clientes", dados => setClientes(dados), erro => setMensagemErro(erro));
        api.get("produtos", dados => setProdutos(dados), erro => setMensagemErro(erro));
    }, []);

    const registrarCompra = (e) => {
        e.preventDefault();

        // Validação de estoque
        const produto = produtos.find(p => p.id === parseInt(produtoId));
        if (produto && produto.estoque < quantidade) {
            setMensagemErro("Estoque insuficiente para realizar a compra.");
            return;
        }

        // Enviar os dados para a API
        const compra = {
            ClienteId: clienteId,
            ProdutoId: produtoId,
            Quantidade: quantidade
        };

        api.post("compras", compra, 
            (res) => {
                navigate("/"); 
            }, 
            (erro) => setMensagemErro(erro)
        );
    };

    return (
        <div className="p-3">
            <h3>Registrar Compra</h3>
            {mensagemErro && <div className="alert alert-danger">{mensagemErro}</div>}
            
            <form onSubmit={registrarCompra}>
                <div className="mb-3">
                    <label className="form-label">Cliente</label>
                    <select 
                        className="form-control"
                        value={clienteId}
                        onChange={(e) => setClienteId(e.target.value)}
                    >
                        <option value="">Selecione um Cliente</option>
                        {clientes.map(cliente => (
                            <option key={cliente.id} value={cliente.id}>
                                {cliente.nome}
                            </option>
                        ))}
                    </select>
                </div>
                
                <div className="mb-3">
                    <label className="form-label">Produto</label>
                    <select 
                        className="form-control"
                        value={produtoId}
                        onChange={(e) => setProdutoId(e.target.value)}
                    >
                        <option value="">Selecione um Produto</option>
                        {produtos.map(produto => (
                            <option key={produto.id} value={produto.id}>
                                {produto.nome} - {produto.preco} (Estoque: {produto.estoque})
                            </option>
                        ))}
                    </select>
                </div>

                <div className="mb-3">
                    <label className="form-label">Quantidade</label>
                    <input 
                        type="number"
                        className="form-control"
                        value={quantidade}
                        min="1"
                        onChange={(e) => setQuantidade(e.target.value)}
                    />
                </div>

                <button className="btn btn-primary" type="submit">Registrar Compra</button>
                <button className="btn btn-secondary" onClick={() => navigate("/")}>Voltar</button>
            </form>
        </div>
    );
};

export default CompraPage;
