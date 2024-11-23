import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../utils/api";

const CadastroPage = () => {
    const [username, setUserName] = useState("");
    const [senha, setSenha] = useState("");
    const [mensagemErro, setMensagemErro] = useState(null);
    const navigate = useNavigate();

    const registrarUsuario = (e) => {
        e.preventDefault();
        const novoUsuario = { username, senha };

        api.post("usuarios", novoUsuario,
            () => navigate("/login"),
            (erro) => setMensagemErro(erro)
        );
    };

    return (
        <div className="p-3">
            <h3>Cadastro de Usuário</h3>
            {mensagemErro && <div className="alert alert-danger">{mensagemErro}</div>}
            <form onSubmit={registrarUsuario}>
                <div className="mb-3">
                    <label className="form-label">Username</label>
                    <input
                        type="text"
                        className="form-control"
                        value={username}
                        onChange={(e) => setUserName(e.target.value)}
                    />
                </div>
                <div className="mb-3">
                    <label className="form-label">Senha</label>
                    <input
                        type="password"
                        className="form-control"
                        value={senha}
                        onChange={(e) => setSenha(e.target.value)}
                    />
                </div>
                <button className="btn btn-primary" type="submit">Cadastrar</button>
            </form>
            <div className="mt-3">
                <button
                    className="btn btn-secondary"
                    onClick={() => navigate("/login")}
                >
                    Voltar para Login
                </button>
            </div>
        </div>
    );
};

export default CadastroPage;
