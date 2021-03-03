import React from "react";
import MainLayout from "../layouts/MainLayout";
import {Button, Card, Col, Container, Row, Label} from "reactstrap";
import { AvForm, AvField, AvRadioGroup, AvRadio } from "availity-reactstrap-validation";
import swal from '@sweetalert/with-react'

export default function CreateQuiz() {

    const handleSubmit = (e, data) => {
        e.preventDefault();
        console.log(data);

        swal({
            title: "Atenção!",
            icon: "warning",
            text: "Copie e guarde os códigos/links do seu inquérito num lugar seguro. Caso perca os" +
                " códigos/links não terá como aceder ou editar seu inquérito. ",
            content: (
                <div>
                    <Row>
                        <p>
                            <Label className="text-black-50">Código de Edição (privado, usado para editar questões, visualizar todas respostas)</Label>
                            <span>edit-kjnjvjbjsdkbvjdbvdfkvbkfbjv</span>
                        </p>
                        <p>
                            <Label>Link de Edição (privado, usado para editar questões, visualizar todas respostas)</Label>
                            <span>http://pesquisapp.com/edit-kjnjvjbjsdkbvjdbvdfkvbkfbjv</span>
                        </p>
                    </Row>
                    <Row>
                        <p>
                            <Label>Codigo de visualização (Público, usado por seus inquiridos para enviar respostas)</Label>
                            <span>quiz-kjnjvjbjsdkbvjdbvdfkvbkfbjv</span>
                        </p>
                        <p>
                            <Label>Link de visualização (Público, usado por seus inquiridos para enviar respostas)</Label>
                            <span>http://pesquisapp.com/quiz-kjnjvjbjsdkbvjdbvdfkvbkfbjv</span>
                        </p>
                    </Row>
                </div>
            )
        })
    }

    return(
        <MainLayout>
            <section className="section-profile-cover section-shaped my-0">
                {/* Circles background */}
                <div className="shape shape-style-1 shape-default alpha-4">
                    <span />
                    <span />
                    <span />
                    <span />
                    <span />
                    <span />
                    <span />
                </div>
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
            <section className="section">
                <Container>
                    <Card className="card-profile shadow mt--300 px-5 py-4">
                        <Row className="justify-content-center">
                            <h1>Criar Inquérito</h1>
                        </Row>
                        <Row>
                            <Col sm={12}>
                                <AvForm onValidSubmit={handleSubmit}>
                                    <Row>
                                        <Col md={6}>
                                            <AvField
                                                label="Título do Inquérito"
                                                type="text"
                                                name="title"
                                                required
                                                validate={{
                                                    required: {
                                                        errorMessage: "Campo Obrigatório.",
                                                    },
                                                    minLength: {
                                                        value: 5,
                                                        errorMessage: "Insira no mínimo 5 caracteres.",
                                                    }
                                                }}
                                            />
                                        </Col>
                                        <Col md={6}>
                                            <AvField label="Categoria" type="select" name="category">
                                                <option>1</option>
                                                <option>2</option>
                                                <option>3</option>
                                                <option>4</option>
                                                <option>5</option>
                                            </AvField>
                                        </Col>
                                    </Row>
                                    <Row>
                                        <Col md={12}>
                                            <AvField
                                                label="Descrição"
                                                type="text"
                                                name="description"
                                            />
                                        </Col>
                                    </Row>
                                    <Row>
                                        <AvRadioGroup name="publicQuiz" label="Privacidade do Inquérito" required>
                                            <AvRadio label="Privado (somente pessoas com o link/código podem aceder ao inquérito)" value="false" />
                                            <AvRadio label="Publico (qualquer pessoa com aceder ao inquérito)" value="true" />
                                        </AvRadioGroup>
                                        <br/>
                                        <AvRadioGroup name="publicAnswer" label="Privacidade das Respostas" required>
                                            <AvRadio label="Privado (Somente pessoas com o link/código de edição podem visualizar as respostas)" value="false" />
                                            <AvRadio label="Publico (Qualquer pessoa que responder o inquérito pode visualizar as respostas)" value="true" />
                                        </AvRadioGroup>
                                    </Row>
                                    <Row className="justify-content-end">
                                        <Button color="primary" type="submit">
                                            Criar
                                        </Button>
                                    </Row>
                                </AvForm>
                            </Col>
                        </Row>
                    </Card>
                </Container>
            </section>
        </MainLayout>
    )
}
