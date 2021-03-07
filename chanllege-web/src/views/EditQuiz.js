import React, {useEffect, useState} from "react";
import {useDispatch, useSelector} from "react-redux";
import {Button, Card, Modal, ModalBody, ModalFooter, ModalHeader, Row,Spinner, Col } from "reactstrap";
import CardLayout from "../layouts/CardLayout";
import {getInputTypes} from "../store/actions/inputTypes.action";
import Question from "../components/Generic/Question";

export default function EditQuiz() {
    const [modal, setModal] = useState(false);
    const dispatch = useDispatch();

    const showQuizCodes = useSelector(state => state.settings.showQuizCodes);
    const inputs = useSelector(state => state.inputType.inputs);
    const isLoading = useSelector(state => state.inputType.isLoading);

    console.log(inputs);

    useEffect(()=> {
        dispatch(getInputTypes());

        if(showQuizCodes)
            toggle();
    }, []);

    const toggle = () => setModal(!modal);

    if(isLoading) {
        return (
          <CardLayout
            headerTitle="Carregando..."
          >
              <Card className="card-profile shadow mt--300 px-5 py-4">
                  <Spinner color="primary" size="lg"/>
              </Card>
          </CardLayout>
        );
    }

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

            <Question inputs={inputs}/>

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
