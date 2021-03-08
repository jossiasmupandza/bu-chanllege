import React, {useState} from "react";
import {Button, Card, Col, Row} from "reactstrap";
import {AvCheckbox, AvCheckboxGroup, AvField, AvForm, AvRadio, AvRadioGroup} from "availity-reactstrap-validation";
import {useDispatch} from "react-redux";
import {removeElementFromArray} from "../../store/actions/questions.action";

export default function Question(props) {
    const {inputs, toggleShowButtons} = props;

    const [selectedInput, setSelectedInput] = useState('Text');
    const [question, setQuestion] = useState("Exemplo?");
    const [options, setOptions] = useState([1]);

    const dispatch = useDispatch();

    const removeLastOption = () => {
        let ops = [...options];
        ops.pop();
        setOptions(ops);
    }

    const removeQuestionHandler = ()=> {
        dispatch(removeElementFromArray());
        toggleShowButtons();
    }

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
                    (selectedInput === "ComboBox" || selectedInput === "Select" || selectedInput === "CheckBox") &&
                    <div className="mt-4">
                        {
                            options?.map((option, index)=> (
                                <Row className="mb-2">
                                    <Col xs={12} className>
                                        <AvField
                                            label={"Opcao "+(index+1)}
                                            type="text"
                                            name="opcao"
                                            required
                                            validate={{
                                                required: {
                                                    errorMessage: "Campo Obrigatório.",
                                                }
                                            }}
                                        />
                                    </Col>
                                </Row>
                            ))
                        }
                        <Row className="justify-content-start mb-5">
                            <Col sm={12}>
                                <Button color="success" onClick={()=> setOptions([...options, options.length])}>Adicionar Opcao</Button>
                                <Button color="light" onClick={removeLastOption}>Remover Opcao</Button>
                            </Col>
                        </Row>
                    </div>
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
                            selectedInput === "Text" &&
                            <AvField
                                label={question}
                                type="text"
                                name="exemplo"
                            />
                        }

                        {
                            selectedInput === "TextArea" &&
                            <AvField
                                label={question}
                                type="textarea"
                                name="exemplo"
                            />
                        }

                        {
                            selectedInput === "ComboBox" &&
                            <AvRadioGroup name="exampleRadio" label={question}>
                                {
                                    options?.map((q, index)=> (
                                        <AvRadio label={"Opcao "+(index+1)} />
                                    ))
                                }
                            </AvRadioGroup>
                        }

                        {
                            selectedInput === "CheckBox" &&
                            <AvCheckboxGroup name="exemplo" label={question}>
                                {
                                    options?.map((q, index)=> (
                                        <AvCheckbox label={"Opcao "+(index+1)} />
                                    ))
                                }
                            </AvCheckboxGroup>
                        }

                        {
                            selectedInput === "Date" &&
                            <AvField name="exemplo" label={question} type="date" />
                        }

                        {
                            selectedInput === "Time" &&
                            <AvField name="exemplo" label={question} type="time" />
                        }

                        {
                            selectedInput === "Select" &&
                            <AvField
                                label={question}
                                type="select"
                                name="exemplo"
                            >
                                {
                                    options?.map((q, index)=> (
                                        <option>{"Opcao "+(index+1)}</option>
                                    ))
                                }
                            </AvField>

                        }
                    </Col>
                </Row>
                <Row className="justify-content-end mt-3 mb-4 px-3">
                    <Button color="light" onClick={removeQuestionHandler}>Remover</Button>
                    <Button type="submit" color="primary">Salvar</Button>
                </Row>
            </AvForm>
        </Card>
    );
}
