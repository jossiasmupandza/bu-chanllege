import React, {useEffect, useState} from "react";
import {useDispatch, useSelector} from "react-redux";
import {Button, Card, Modal, ModalBody, ModalFooter, ModalHeader, Row,Spinner, Col, Container, UncontrolledTooltip} from "reactstrap";
import CardLayout from "../layouts/CardLayout";
import {getInputTypes} from "../store/actions/inputTypes.action";
import Question from "../components/Generic/Question";
import {addElementInArray} from "../store/actions/questions.action";

export default function EditQuiz() {
    const [modal, setModal] = useState(false);
    const [showButtons, setShowButtons] = useState(true);
    const [showLinks, setShowLinks] = useState(false);
    const dispatch = useDispatch();

    const showQuizCodes = useSelector(state => state.settings.showQuizCodes);
    const inputs = useSelector(state => state.inputType.inputs);
    const isLoading = useSelector(state => state.inputType.isLoading);
    const questions = useSelector(state => state.question.questions);


    useEffect(()=> {
        dispatch(getInputTypes());

        if(showQuizCodes)
            toggle();
    }, []);

    const toggle = () => setModal(!modal);

    const toggleShowButtons = () => {
        setShowButtons(!showButtons);
    }

    const toggleShowLinks = () => {
        setShowLinks(!showLinks);
    }

    const addQuestionHandler = () => {
        dispatch(addElementInArray());
        toggleShowButtons();
    }

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

                        <Button className="mb-3 mt-3" onClick={toggleShowLinks}>Links...</Button>
                        {
                            showLinks &&
                            <>
                                <h5>Link do Inquérito</h5>
                                <p>Partilhe este link com o seu publico alvo, para obter respostas.</p>

                                <LinkArea link={"http://pesquisaapp.com/quiz-dlwkdnqndjqnd"}/>

                                <Col className="text-center">
                                    Ou partilhe o código abaixo para aceder através do site
                                </Col>

                                <LinkArea link={"quiz-dlwkdnqndjqnd"}/>

                                <br/>
                                <h5>Link de Proprietário</h5>
                                <p>Use Este link para editar o seu Inquérito e visualizar as respostas enviadas pêlos seus inquiridos.
                                    <span className="text-danger">(Nota: caso perca esse link não terá como aceder ao seu inquérito ).</span></p>

                                <LinkArea link={"http://pesquisaapp.com/edit-wweerrtttyyys"}/>

                                <Col className="text-center">
                                    Ou utilize o código abaixo para aceder através do site
                                </Col>

                                <LinkArea link={"edit-wweerrtttyyys"}/>
                            </>
                        }

                        <p className="text-light">Passo 2 de 2</p>
                    </Col>
                </Row>
            </Card>

            {
                questions?.map((question,index) =>(
                    <Question inputs={inputs} toggleShowButtons={toggleShowButtons}/>
                ))
            }

            {
                showButtons &&
                    <>
                        <Row className="justify-content-end mt-3 mb-4 px-3">
                            <Button color="primary" onClick={addQuestionHandler}>Adicionar Questão</Button>
                        </Row>

                        <Row className="justify-content-center mt-3 mb-4 px-3">
                            <Button color="success" className="btn-block col-4">Publicar Inquérito</Button>
                        </Row>
                    </>
            }


            <Modal isOpen={modal} toggle={toggle} size="lg" backdrop={false}>
                <ModalHeader toggle={toggle}>Atenção: Links do Inquérito</ModalHeader>
                <ModalBody>
                    <h5>Link do Inquérito</h5>
                    <p>Partilhe este link com o seu publico alvo, para obter respostas.</p>

                    <LinkArea link={"http://pesquisaapp.com/quiz-dlwkdnqndjqnd"}/>

                    <Col className="text-center">
                        Ou partilhe o código abaixo para aceder através do site
                    </Col>

                    <LinkArea link={"quiz-dlwkdnqndjqnd"}/>

                    <br/>
                    <h5>Link de Proprietário</h5>
                    <p>Use Este link para editar o seu Inquérito e visualizar as respostas enviadas pêlos seus inquiridos.
                        <span className="text-danger">(Nota: caso perca esse link não terá como aceder ao seu inquérito ).</span></p>

                    <LinkArea link={"http://pesquisaapp.com/edit-wweerrtttyyys"}/>

                    <Col className="text-center">
                        Ou utilize o código abaixo para aceder através do site
                    </Col>

                    <LinkArea link={"edit-wweerrtttyyys"}/>
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={toggle}>Entedido</Button>
                </ModalFooter>
            </Modal>
        </CardLayout>
    )
}

const LinkArea = (props) => {
    const {link} = props;

    const copyToClipBoard = (text) => {
        navigator.clipboard.writeText(text);
    }

    return(
        <Container md={12} className="bg-light rounded mb-3 mt-3" style={{padding: "10px"}}>
            <Row className="justify-content-center">
                <Col sm={11} className="overflow-auto">
                    <span className="float-left">{link}</span>
                </Col>
                <Col sm={1}>
                    <i
                        id="link3"
                        className="fa fa-copy cursor-pointer float-right"
                        onClick={()=> copyToClipBoard(link)}
                        style={{fontSize:"20px"}}
                    />
                </Col>
            </Row>
            <UncontrolledTooltip placement="bottom" target="link3">
                Copy
            </UncontrolledTooltip>
        </Container>
    )
}
