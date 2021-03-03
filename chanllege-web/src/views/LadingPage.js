import React from "react";
import MainLayout from "../layouts/MainLayout";
import {Button, Col, Container, Row} from "reactstrap";
import {Link} from "react-router-dom";


export default function LadingPage(props) {
    return (
        <MainLayout>
            <div className="position-relative">

                {/* shape Hero */}
                <section className="section section-lg section-shaped pb-250">
                    <div className="shape shape-style-1 shape-default">
                        <span />
                        <span />
                        <span />
                        <span />
                        <span />
                        <span />
                        <span />
                        <span />
                        <span />
                    </div>
                    <Container className="py-lg-md d-flex">
                        <div className="col px-0">
                            <Row>
                                <Col lg="6">
                                    <h1 className="display-3 text-white">
                                        Pesquisa App{" "}
                                        <span>Crie, edite e partilhe seus inquéritos de forma pratica</span>
                                    </h1>
                                    <p className="lead text-white">
                                        Não perca tempo, crie seu inquérito ou responda  a pesquisas privada ou públicas. Basta um clique. 😊
                                    </p>
                                    <div className="btn-wrapper">
                                        <Button
                                            className="btn-icon mb-3 mb-sm-0"
                                            color="info"
                                            tag={Link}
                                            to="/responder"
                                        >
                                          <span className="btn-inner--icon mr-1">
                                            <i className="fa fa-check-square-o" />
                                          </span>
                                            <span className="btn-inner--text">Responder Inquérito</span>
                                        </Button>
                                        <Button
                                            className="btn-white btn-icon mb-3 mb-sm-0 ml-1"
                                            color="default"
                                            tag={Link}
                                            to="/criar"
                                        >
                                          <span className="btn-inner--icon mr-1">
                                            <i className="fa fa-file-o" />
                                          </span>
                                            <span className="btn-inner--text">
                                            Criar Inquérito
                                          </span>
                                        </Button>
                                    </div>
                                </Col>
                            </Row>
                        </div>
                    </Container>

                    {/* SVG separator */}
                    <div className="separator separator-bottom separator-skew">
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            preserveAspectRatio="none"
                            version="1.1"
                            viewBox="0 0 2560 100"
                            x="0"
                            y="0"
                        >
                            <polygon
                                className="fill-white"
                                points="2560 0 2560 100 0 100"
                            />
                        </svg>
                    </div>
                </section>
                {/* 1st Hero Variation */}
            </div>
        </MainLayout>
    )
}
