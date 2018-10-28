import React from 'react';
import { connect } from 'react-redux';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import { Link } from 'react-router-dom';

export class Survey extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        const survey = this.props.survey;
        const showLinks = this.props.withLinks !== false;
        if (survey !== null) {
            return (
                <Card>
                    <CardContent>
                        <Typography color="textSecondary" gutterBottom>Catalogo</Typography>
                        <Typography variant="h5" component="h2">{survey.name}</Typography>
                        <Typography color="textSecondary">nessun elemento nella raccolta</Typography>
                        <Typography component="p">
                            Descrizione, da popolare...
                    </Typography>
                    </CardContent>
                    {showLinks &&
                        <CardActions>
                            <Link to={`/survey/edit/${survey.id}`}>
                                <Button size="small">Configura campi</Button>
                            </Link>
                        </CardActions>
                    }
                </Card>
            );
        } else {
            return <div />;
        }
    }
}
