import React, from "react";
import {useHistory} from "react-router-dom";
import { AvForm, AvField, AvRadioGroup, AvRadio } from "availity-reactstrap-validation";
import swal from '@sweetalert/with-react';
import {
    Button,
    Col,
    Row,
} from "reactstrap";
import {useDispatch} from "react-redux";
import {toggleModalShowQuizCodes} from "../store/actions/settings.action";
import CardLayout from "../layouts/CardLayout";

export default function CreateQuiz() {
    const history = useHistory();
    const dispatch = useDispatch()

    const handleSubmit = (e, data) => {
        e.preventDefault();
        console.log(data);

        swal({
            title: "Inquérito Criado!",
            icon: "success",
            text: "Passo 1 concluído, clique em ‘ok’ para continuar e adicionar  questões no seu inquérito."
        }).then(()=> {
            dispatch(toggleModalShowQuizCodes(true));
            history.push("/edit-quiz");
        });
    }

    return(
        <CardLayout title="Criar Inquérito">
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
                    <Row className="justify-content-between">
                                        <span className="text-light">
                                            Passo 1 de 2
                                        </span>
                        <Button color="primary" type="submit">
                            Criar
                        </Button>
                    </Row>
                </AvForm>
            </Col>
        </CardLayout>
    )
}
