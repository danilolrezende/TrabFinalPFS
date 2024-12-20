import React, { useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import api from "../../utils/api";

const Login = () => {
    const [objeto, setObjeto] = useState(
        { username: "", senha: '' }
    );
    const [falha, setFalha] = useState(null);
    const navigate = useNavigate();
    const { redirecionarPara } = useParams();

    const atualizarCampo = (nome, valor) => {
        let objNovo = { ...objeto };
        objNovo[nome] = valor;
        setObjeto(objNovo);
    };

    const sucessoLogin = (usuario) => {
        localStorage.setItem("usuario-nome", usuario.username);

        if (!redirecionarPara) {
            navigate(0);
        } else {
            const redirecionar = atob(redirecionarPara);
            document.location.href = redirecionar;
        }
    };

    const login = e => {
        e.preventDefault();
        api.post('login/navegador', objeto, sucessoLogin, setFalha);
    }

    let mensagemFalha = null;

    if (falha) {
        mensagemFalha = (<div className="alert alert-danger">{falha}</div>);
        setTimeout(() => {
            setFalha(null);
        }, 10000);
    }

    return (
        <div className="">
            {mensagemFalha}
            <div className="d-flex justify-content-center">
                <form className="w-50 m-5">
                    <h3>Login</h3>
                    <div>
                        <label className="form-label">Username</label>
                        <input className="form-control" value={objeto.username} onChange={e => atualizarCampo('username', e.target.value)} type="text" />
                    </div>
                    <div>
                        <label className="form-label">Senha</label>
                        <input className="form-control" value={objeto.senha} onChange={e => atualizarCampo('senha', e.target.value)} type="password" />
                    </div>
                    <button className="btn btn-primary mt-2" onClick={e => login(e)}>Login</button>
                </form>
                <div className="mt-3">
                    <button
                        className="btn btn-secondary"
                        onClick={() => navigate("/cadastro")}
                    >
                        Cadastrar Usuário
                    </button>
                </div>
            </div>
        </div>
    );
};

export default Login;