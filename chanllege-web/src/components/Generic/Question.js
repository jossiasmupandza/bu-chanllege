import React, {useState} from "react";
import {Button, Card, Col, Row} from "reactstrap";
import {AvCheckbox, AvCheckboxGroup, AvField, AvForm, AvRadio, AvRadioGroup} from "availity-reactstrap-validation";

export default function Question(props) {
    const {inputs} = props;
    const [selectedInput, setSelectedInput] = useState('Text');
    const [question, setQuestion] = useState("Exemplo");

    return (
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
                            value={question}
                            onChange={e => setQuestion(e.target.value)}
                            validate={{
                                required: {
                                    errorMessage: "Campo Obrigatório.",
                                }
                            }}
                        />
                    </Col>
                    <Col md={6}>
                        <AvField
                            label="Formato da Resposta"
                            type="select"
                            name="category"
                            onChange={e => setSelectedInput(e.target.value)}
                        >
                            {
                                inputs.map(input => (
                                    <option key={input.id} value={input.type}>{input.description}</option>
                                ))
                            }
                        </AvField>
                    </Col>
                </Row>
                <Row>
                    <Col md={6}>
                        <AvCheckboxGroup name="QuestionRequireds" >
                            <AvCheckbox label="Resposta Não Obrigatória" name="QuestionRequired"/>
                        </AvCheckboxGroup>
                    </Col>
                </Row>

                {
                    (selectedInput == "ComboBox" || selectedInput == "Select" || selectedInput == "CheckBox") &&
                    <>
                        <Row className="mt-4">
                            <Col xs={10} className>
                                <AvField
                                    label="Opcao 1"
                                    type="text"
                                    name="opcao1"
                                    required
                                    validate={{
                                        required: {
                                            errorMessage: "Campo Obrigatório.",
                                        }
                                    }}
                                />
                            </Col>
                            <Col xs={2} className="mt-4">
                                <i className="fa fa-close display-3"/>
                            </Col>
                        </Row>
                        <Row className="justify-content-start mb-5">
                            <Col sm={12}>
                                <Button color="success">Adicionar Opcao</Button>
                            </Col>
                        </Row>
                    </>
                }

                <Row>
                    <Col md={12}>
                        <h5>Pré-Visualização</h5>
                        <hr/>
                    </Col>
                </Row>
                <Row className="justify-content-center ">
                    <Col sm={10} className="border">
                        {
                            selectedInput == "Text" &&
                            <AvField
                                label={question}
                                type="text"
                                name="exemplo"
                            />
                        }

                        {
                            selectedInput == "TextArea" &&
                            <AvField
                                label={question}
                                type="textarea"
                                name="exemplo"
                                required
                            />
                        }

                        {
                            selectedInput == "ComboBox" &&
                            <AvRadioGroup name="exampleRadio" label={question} required>
                                <AvRadio label="opcao 1" value="false" />
                                <AvRadio label="Opcao 2" value="true" />
                            </AvRadioGroup>
                        }

                        {
                            selectedInput == "CheckBox" &&
                            <AvCheckboxGroup name="exemplo" label={question}>
                                <AvCheckbox label="opcao 1"/>
                                <AvCheckbox label="opcao 2"/>
                            </AvCheckboxGroup>
                        }

                        {
                            selectedInput == "Date" &&
                            <AvField name="exemplo" label={question} type="date" />
                        }

                        {
                            selectedInput == "Time" &&
                            <AvField name="exemplo" label={question} type="time" />
                        }

                        {
                            selectedInput == "Select" &&
                            <AvField
                                label={question}
                                type="select"
                                name="exemplo"
                            >
                                <option>opcao 1</option>
                                <option>opcao 2</option>
                            </AvField>

                        }
                    </Col>
                </Row>
                <Row className="justify-content-end mt-3 mb-4 px-3">
                    <Button color="light">Remover</Button>
                    <Button color="primary">Salvar</Button>
                </Row>
            </AvForm>
        </Card>
    );
}
