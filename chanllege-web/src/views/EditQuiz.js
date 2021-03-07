import React, {useEffect, useState} from "react";
import {useSelector} from "react-redux";
import { AvForm, AvField, AvCheckboxGroup, AvCheckbox } from "availity-reactstrap-validation";
import {Button, Card, Modal, ModalBody, ModalFooter, ModalHeader, Row, Col } from "reactstrap";
import CardLayout from "../layouts/CardLayout";

export default function EditQuiz() {
    const [modal, setModal] = useState(false);

    const showQuizCodes = useSelector(state => state.settings.showQuizCodes)

    useEffect(()=> {
        if(showQuizCodes)
            toggle();
    }, []);

    const toggle = () => setModal(!modal);

    return (
        <CardLayout
            headerTitle="Adicionar Questõeslaksjkedjlwke ekkkkkkkkkkkkkkk eeeeee eeeeee eeeeee e eeeeee eeee eeeee eeee eeeeee ee e e e e e e e e e e e e e e e e e"
        >
            <Card className="card-profile shadow mt--300 px-5 py-4">
                <Row className="justify-content-center">
                    <h1>Editar Questões</h1>
                </Row>
                <Row>
                    <Col sm={12}>
                        <p className="text-light"><span className="text-dark">Título do Inquérito:</span> aaaa</p>
                        <p className="text-light"><pan className="text-dark">Descrição:</pan> dsddsd</p>
                        <p className="text-light"><pan className="text-dark">Tipo de Inquérito:</pan>privado</p>
                        <p className="text-light"><pan className="text-dark">Tipo de Respostas:</pan>privado</p>
                        <p className="text-light">Passo 2 de 2</p>
                    </Col>
                </Row>
            </Card>

            <Card className=" shadow px-5 py-4 my-4">
                <AvForm>
                    <Row>
                        <Col md={6}>
                            <AvField
                                label="Questão"
                                type="text"
                                name="title"
                                required
                                placeholder="Exemplo?"
                                validate={{
                                    required: {
                                        errorMessage: "Campo Obrigatório.",
                                    }
                                }}
                            />
                        </Col>
                        <Col md={6}>
                            <AvField label="Formato da Resposta" type="select" name="category">
                                <option>1</option>
                                <option>2</option>
                                <option>3</option>
                                <option>4</option>
                                <option>5</option>
                            </AvField>
                        </Col>
                    </Row>
                    <Row>
                        <AvCheckboxGroup name="checkboxExample" label="Resposta Obrigatória">
                            <AvCheckbox label="Bulbasaur" value="Bulbasaur" />
                        </AvCheckboxGroup>
                    </Row>
                </AvForm>
            </Card>


            <Row className="justify-content-end mt-3 mb-4 px-3">
                <Button color="primary">Adicionar Questão</Button>
            </Row>

            <Row className="justify-content-center mt-3 mb-4 px-3">
                <Button color="success" className="btn-block col-4">Publicar Inquérito</Button>
            </Row>


            <Modal isOpen={modal} toggle={toggle}>
                <ModalHeader toggle={toggle}>Códigos do Inquérito</ModalHeader>
                <ModalBody>
                    m ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={toggle}>Entedido</Button>
                </ModalFooter>
            </Modal>
        </CardLayout>
    )
}
