import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { surveyActionCreators } from '../../store/SurveyStore';
import SurveyPresentationComponent from './SurveyPresentationComponent';
import QuestionComponent from '../Questions/QuestionComponent';
import WithLoading from '../LoadingHOC';
import Grid from '@material-ui/core/Grid';
import Box from '@material-ui/core/Box';

class SurveyDetailRoutedComponent extends React.Component {

    sendMutation(questionId, value) {
        this.props.requestSurveyQuestionMutation(this.props.survey.id, questionId, value);
    }

    render() {
            console.log("drown");
        return (
            <div><p>{this.props.foo}</p>
                <Box marginBottom={4}>
                    <SurveyPresentationComponent survey={this.props.survey} withlinks={false} raised />
                </Box>
                <Grid container direction="column" spacing={2}>
                    {this.props.survey.questions.map(question =>
                        (<Grid item key={question.id}>
                            <QuestionComponent {...question} sendMutation={(val) => this.sendMutation(question.id, val)} />
                        </Grid>))}
                </Grid>
            </div>
        );
    }
}

const SurveyDetailRoutedComponentWithLoading = WithLoading(SurveyDetailRoutedComponent, (_context) => _context.props.requestSurvey(parseInt(_context.props.match.params.id)));
export default connect(
    state => state.surveyReducer,
    surveyActionCreators)(SurveyDetailRoutedComponentWithLoading);