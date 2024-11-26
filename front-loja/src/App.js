import { BrowserRouter, Routes, Route } from "react-router-dom";
import React from 'react';
import ProdutoListar from './components/produtos/ProdutoListar';
import ProdutoInserir from './components/produtos/ProdutoInserir';
import ProdutoConsultar from './components/produtos/ProdutoConsultar';
import ProdutoAlterar from './components/produtos/ProdutoAlterar';
import Home from './components/principal/Home';
import ClienteListar from './components/clientes/ClienteListar';
import ClienteInserir from './components/clientes/ClienteInserir';
import ClienteConsultar from './components/clientes/ClienteConsultar';
import ClienteAlterar from './components/clientes/ClienteAlterar';
import Login from "./components/acessos/Login";
import CompraPage from "./components/compras/Compra";
import CompraListPage from "./components/compras/CompraListar";
import CadastroPage from "./components/acessos/Cadastro";

function App() {
  return (
    <BrowserRouter>
    <Routes>
      <Route path="/" element={<Home />} />
      {/* <Route path="/login/:redirecionarPara" element={<Login />} /> */}
      <Route path="/produtos" element={<ProdutoListar />} />
      <Route path="/produtos/listar" element={<ProdutoListar />} />
      <Route path="/produtos/inserir" element={<ProdutoInserir />} />
      <Route path="/produtos/consultar/:id" element={<ProdutoConsultar />} />
      <Route path="/produtos/alterar/:id" element={<ProdutoAlterar />} />
      <Route path="/clientes" element={<ClienteListar />} />
      <Route path="/clientes/listar" element={<ClienteListar />} />
      <Route path="/clientes/inserir" element={<ClienteInserir />} />
      <Route path="/clientes/consultar/:id" element={<ClienteConsultar />} />
      <Route path="/clientes/alterar/:id" element={<ClienteAlterar />} />
      <Route path="/compras" element={<CompraPage />} />
      <Route path="/compras/cliente/:clienteId" element={<CompraListPage />} />      
      <Route path="/cadastro" element={<CadastroPage />} />    
    </Routes>
  </BrowserRouter>
  );
}

export default App;
