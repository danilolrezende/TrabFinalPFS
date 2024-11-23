import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import Logout from "../acessos/Logout";

const Home = () => {
    const navigate = useNavigate();
    const [falha, setFalha] = useState(null);


    const irParaProdutos = () => navigate("/produtos");
    const irParaUsuarios = () => navigate("/clientes");
    const irParaCompras = () => navigate("/compras");

    return (
        <div className="container text-center mt-5">
            <div className="d-flex justify-content-between">
                <Logout setFalha={setFalha} />
            </div>
            <h1>Bem-vindo à Loja</h1>
            <p>Escolha uma opção para navegar:</p>
            <div className="d-flex justify-content-center gap-3 mt-4">
                <button className="btn btn-primary" onClick={irParaProdutos}>
                    Listar Produtos
                </button>
                <button className="btn btn-secondary" onClick={irParaUsuarios}>
                    Listar Clientes
                </button>
                <button className="btn btn-success" onClick={irParaCompras}>
                    Realizar Compras
                </button>
            </div>
        </div>
    );
};

export default Home;
