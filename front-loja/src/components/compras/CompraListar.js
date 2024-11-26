import React, { useState, useEffect } from "react";
import api from "../../utils/api";
import { useNavigate, useParams } from "react-router-dom";

const CompraListPage = () => {
    const { clienteId } = useParams(); // Pega o ID do cliente da URL
    const [compras, setCompras] = useState([]);
    const [cliente, setCliente] = useState(null);
    const [mensagemErro, setMensagemErro] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        // Carregar as compras para o cliente
        api.get(`compras/cliente/${clienteId}`, dados => {
            setCompras(dados);
        }, erro => setMensagemErro(erro));

        // Carregar dados do cliente
        api.get(`clientes/${clienteId}`, dados => {
            setCliente(dados);
        }, erro => setMensagemErro(erro));
    }, [clienteId]);

    return (
        <div className="p-3">
            <h3>Compras do Cliente {cliente?.nome}</h3>

            {mensagemErro && <div className="alert alert-danger">{mensagemErro}</div>}

            <div>
                {compras.length === 0 ? (
                    <p>Este cliente não fez nenhuma compra ainda.</p>
                ) : (
                    <table className="table">
                        <thead>
                            <tr>
                                <th scope="col">Produto</th>
                                <th scope="col">Quantidade</th>
                                <th scope="col">Preço Total</th>
                            </tr>
                        </thead>
                        <tbody>
                            {compras.map(compra => (
                                <tr key={compra.id}>
                                    <td>{compra.produto.nome}</td>
                                    <td>{compra.quantidade}</td>
                                    <td>{(compra.produto.preco * compra.quantidade).toFixed(2)}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                )}
            </div>
            <button className="btn btn-primary" onClick={() => navigate("/clientes")}>Voltar</button>
            <button className="btn btn-secondary" onClick={() => navigate("/")}>Voltar para a Página Principal</button>
        </div>
    );
};

export default CompraListPage;
